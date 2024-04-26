import React, { useState, useEffect, useMemo } from "react";
import axios from "axios";
import { useNavigate } from "react-router-dom";
import Box from "@mui/material/Box";
import Typography from "@mui/material/Typography";
import TextField from "@mui/material/TextField";
import Modal from "@mui/material/Modal";
import Button from "@mui/material/Button";
import CloseIcon from "@mui/icons-material/Close";
import { FormControl, InputLabel, Select, MenuItem } from "@mui/material";

import {
  fetchPatients,
  addPatient,
  updatePatient,
  deletePatient,
} from "../../Api/PatientApi";
import PatientCard from "./PatientCard";
import PatientTable from "./PatientTable";

function Patient({ isSignedIn }) {
  const [patients, setPatients] = useState([]);
  const [openModal, setOpenModal] = useState(false);
  const [selectedPatient, setSelectedPatient] = useState(null);
  const [newPatientData, setNewPatientData] = useState({
    name: "",
    dateOfBirth: "",
    email: "",
    phoneNo: "",
    medicalHistory: "",
  });
  const [errorMessage, setErrorMessage] = useState("");
  const navigateTo = useNavigate();
  const [showGrid, setShowGrid] = useState(false);
  const [searchText, setSearchText] = useState("");

  useEffect(() => {
    if (!isSignedIn) {
      navigateTo("/SignIn");
    }
  }, []);

  const handleOpenModal = () => {
    setOpenModal(true);
  };

  const handleCloseModal = () => {
    setNewPatientData({
      name: "",
      dateOfBirth: "",
      email: "",
      phoneNo: "",
      medicalHistory: "",
    });
    setSelectedPatient(null);
    setErrorMessage("");

    console.log(newPatientData);
    setOpenModal(false);
  };

  const handleOpenUpdateModal = (patient) => {
    setSelectedPatient(patient);
    setNewPatientData({
      ...patient,
    });
    console.log(patient);
    setOpenModal(true);
  };

  const handleInputChange = (name, value) => {
    setNewPatientData((prevData) => ({
      ...prevData,
      [name]: value,
    }));
  };

  const handleDeletePatient = (patientId) => {
    if (window.confirm("Are you sure you want to delete this patient?")) {
      deletePatient(patientId)
        .then((response) => {
          setPatients((prevList) =>
            prevList.filter((patient) => patient.id !== patientId)
          );
        })
        .catch((error) => {
          console.error("Error deleting patient:", error.message);
          alert("Failed to remove appointment");
        });
    }
  };

  const handleAddPatient = () => {
    if (validateForm()) {
      let id;
      if (patients.length === 0) {
        id = 1;
      } else {
        id = parseInt(patients[patients.length - 1].id) + 1;
        id = id.toString();
      }
      addPatient({ id: id, ...newPatientData })
        .then((response) => {
          console.log(response);
          alert("New Patient added ");
          handleCloseModal();
        })
        .catch((error) => {
          console.error("Error adding patient:", error.message);
          alert("Failed to add new patient ");
        });
    }
  };

  const handleUpdatePatient = () => {
    const updatedPatient = {
      ...selectedPatient,
      ...newPatientData,
    };

    updatePatient(updatedPatient.id, updatedPatient)
      .then((response) => {
        console.log("Patient updated successfully:", response.data);
        handleCloseModal();
        setPatients((prev) =>
          prev.map((patient) => {
            if (patient.id === updatedPatient.id) {
              return updatedPatient;
            } else {
              return patient;
            }
          })
        );

        alert("Patient Updated ");
        setNewPatientData({
          name: "",
          dateOfBirth: "",
          email: "",
          phoneNo: "",
          medicalHistory: "",
        });
      })
      .catch((error) => {
        console.error("Error updating Patient:", error.message);
        alert("Failed to update Patient");
      });
  };

  const validateForm = () => {
    const errors = {};
    if (newPatientData.name === "") {
      errors.name = "Name is required";
    }
    if (newPatientData.dateOfBirth === "") {
      errors.dateOfBirth = "Date of Birth is required";
    }
    if (newPatientData.phoneNo === "") {
      errors.phoneNo = "Phone No is required";
    }
    if (newPatientData.email === "") {
      errors.email = "Email is required";
    }
    setErrorMessage({ ...errors });

    return Object.keys(errors).length < 1;
  };

  const handleSearch = async () => {
    try {
      console.log(searchText);
      const allPatientList = await fetchPatients();
      const searchRegex = new RegExp(searchText, "i"); // 'i' flag for case-insensitive matching
      let matchedPatients = allPatientList.data.filter((patient) =>
        searchRegex.test(patient.name)
      );
      setPatients(matchedPatients);
    } catch (error) {
      console.error("Error filtering patient data:", error);
    }
  };

  useEffect(() => {
    handleSearch();
  }, [searchText]);

  return (
    <div>
      <Box
        sx={{
          display: "flex",
          justifyContent: "space-between",
          alignItems: "center",
          p: 2,
          flexWrap: "wrap",
        }}
        className="patientHeader"
      >
        <Typography variant="h4" id="tableTitle" component="div">
          Patient List
        </Typography>

        <TextField
          label="Search Patient"
          variant="outlined"
          value={searchText}
          onChange={(e) => setSearchText(e.target.value)}
          sx={{ margin: "15px", marginRight: "0px" }}
        />
        <FormControl style={{ float: "right", margin: "15px" }}>
          <InputLabel id="select-label">View Mode</InputLabel>
          <Select
            labelId="select-label"
            value={showGrid ? "grid" : "table"}
            label="View Mode"
            onChange={(e) => {
              setPatients([]);
              setShowGrid(e.target.value === "grid");
            }}
          >
            <MenuItem value="table">Table</MenuItem>
            <MenuItem value="grid">Grid</MenuItem>
          </Select>
        </FormControl>
        <Button
          variant="contained"
          onClick={handleOpenModal}
          style={{ padding: "15px 20px", width: "220px" }}
          className="colorPurple"
        >
          Add Patient
        </Button>
      </Box>
      <div>
        {showGrid ? (
          <PatientCard
            patientList={patients}
            setPatientList={setPatients}
            openModal={openModal}
            setOpenModal={setOpenModal}
            setNewPatientData={setNewPatientData}
            handleDeletePatient={handleDeletePatient}
          />
        ) : (
          <PatientTable
            patientList={patients}
            setPatientList={setPatients}
            openModal={openModal}
            setOpenModal={setOpenModal}
            handleOpenUpdateModal={handleOpenUpdateModal}
            setNewPatientData={setNewPatientData}
          />
        )}
      </div>
      <Modal
        open={openModal}
        onClose={handleCloseModal}
        aria-labelledby="add-patient-modal"
        aria-describedby="add-patient-form"
      >
        <Box
          sx={{
            position: "absolute",
            top: "50%",
            left: "50%",
            transform: "translate(-50%, -50%)",
            width: 400,
            bgcolor: "background.paper",
            border: "2px solid #000",
            boxShadow: 24,
            p: 4,
          }}
        >
          <Box sx={{ display: "flex", justifyContent: "space-between" }}>
            <Typography
              id="modal-modal-title"
              variant="h6"
              component="h2"
              sx={{ marginBottom: 2 }}
            >
              {newPatientData.id ? "Update Patient Details" : "Add New Patient"}
            </Typography>
            <Button
              onClick={handleCloseModal}
              color="error"
              variant="contained"
              sx={{
                p: "8px",
                minWidth: "unset",
                width: "32px",
                height: "32px",
              }}
            >
              <CloseIcon />
            </Button>
          </Box>
          <Box sx={{ marginBottom: 2 }}>
            <TextField
              id="name"
              name="name"
              label="Name *"
              value={newPatientData.name}
              onChange={(e) => handleInputChange("name", e.target.value)}
              fullWidth
            />
            <div className="errorMessage">{errorMessage["name"]}</div>
          </Box>
          <Box sx={{ marginBottom: 2 }}>
            <TextField
              id="dateOfBirth"
              name="dateOfBirth"
              type="date"
              label="Date Of Birth"
              InputLabelProps={{
                shrink: true,
                style: { width: "100px" },
              }}
              value={newPatientData.dateOfBirth}
              onChange={(e) => handleInputChange("dateOfBirth", e.target.value)}
              fullWidth
            />
            <div className="errorMessage">{errorMessage["dateOfBirth"]}</div>
          </Box>

          <Box sx={{ marginBottom: 2 }}>
            <TextField
              id="email"
              name="email"
              label="Email *"
              value={newPatientData.email}
              onChange={(e) => handleInputChange("email", e.target.value)}
              fullWidth
            />
            <div className="errorMessage">{errorMessage["email"]}</div>
          </Box>

          <Box sx={{ marginBottom: 2 }}>
            <TextField
              id="phoneNo"
              name="phoneNo"
              label="Phone No *"
              value={newPatientData.phoneNo}
              onChange={(e) => handleInputChange("phoneNo", e.target.value)}
              fullWidth
            />
            <div className="errorMessage">{errorMessage["phoneNo"]}</div>
          </Box>

          <TextField
            id="medicalHistory"
            name="medicalHistory"
            label="Medical History"
            value={newPatientData.medicalHistory}
            onChange={(e) =>
              handleInputChange("medicalHistory", e.target.value)
            }
            fullWidth
            sx={{ marginBottom: 2 }}
          />
          <Box sx={{ display: "flex", justifyContent: "end" }}>
            <Button
              variant="contained"
              onClick={
                newPatientData.id ? handleUpdatePatient : handleAddPatient
              }
              className="colorPurple"
            >
              {newPatientData.id ? "Update Patient" : "Add Patient"}
            </Button>
          </Box>
        </Box>
      </Modal>
    </div>
  );
}

export default Patient;
