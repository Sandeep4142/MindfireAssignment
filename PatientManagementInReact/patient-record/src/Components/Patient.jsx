import React, { useState, useEffect, useMemo } from "react";
import axios from "axios";
import { useNavigate } from "react-router-dom";
import Box from "@mui/material/Box";
import Table from "@mui/material/Table";
import TableBody from "@mui/material/TableBody";
import TableCell from "@mui/material/TableCell";
import TableContainer from "@mui/material/TableContainer";
import TableHead from "@mui/material/TableHead";
import TablePagination from "@mui/material/TablePagination";
import TableRow from "@mui/material/TableRow";
import TableSortLabel from "@mui/material/TableSortLabel";
import Typography from "@mui/material/Typography";
import Paper from "@mui/material/Paper";
import IconButton from "@mui/material/IconButton";
import { visuallyHidden } from "@mui/utils";
import TextField from "@mui/material/TextField";
import Modal from "@mui/material/Modal";
import Button from "@mui/material/Button";
import CloseIcon from "@mui/icons-material/Close";
import EditIcon from "@mui/icons-material/Edit";
import DeleteIcon from "@mui/icons-material/Delete";
import {
  fetchPatients,
  addPatient,
  updatePatient,
  deletePatient,
} from "../Api/PatientApi";

function descendingComparator(a, b, orderBy) {
  if (b[orderBy] < a[orderBy]) {
    return -1;
  }
  if (b[orderBy] > a[orderBy]) {
    return 1;
  }
  return 0;
}

function getComparator(order, orderBy) {
  return order === "desc"
    ? (a, b) => descendingComparator(a, b, orderBy)
    : (a, b) => -descendingComparator(a, b, orderBy);
}

function stableSort(array, comparator) {
  const stabilizedThis = array.map((el, index) => [el, index]);
  stabilizedThis.sort((a, b) => {
    const order = comparator(a[0], b[0]);
    if (order !== 0) {
      return order;
    }
    return a[1] - b[1];
  });
  return stabilizedThis.map((el) => el[0]);
}

const headCells = [
  { id: "name", numeric: false, disablePadding: true, label: "Name" },
  {
    id: "dateOfBirth",
    numeric: true,
    disablePadding: false,
    label: "Date Of Birth",
  },
  { id: "email", numeric: true, disablePadding: false, label: "Email" },
  { id: "phoneNo", numeric: true, disablePadding: false, label: "Phone No" },
  {
    id: "medicalHistory",
    numeric: true,
    disablePadding: false,
    label: "Medical History",
  },
  { id: "actions", numeric: false, disablePadding: false, label: "Actions" },
];

function EnhancedTableHead(props) {
  const { order, orderBy, onRequestSort } = props;
  const createSortHandler = (property) => (event) => {
    onRequestSort(event, property);
  };

  return (
    <TableHead>
      <TableRow>
        {headCells.map((headCell) => (
          <TableCell
            key={headCell.id}
            align={"left"}
            padding={"normal"}
            sortDirection={orderBy === headCell.id ? order : false}
          >
            <TableSortLabel
              active={orderBy === headCell.id}
              direction={orderBy === headCell.id ? order : "asc"}
              onClick={createSortHandler(headCell.id)}
            >
              {headCell.label}
              {orderBy === headCell.id ? (
                <Box component="span" sx={visuallyHidden}>
                  {order === "desc" ? "sorted descending" : "sorted ascending"}
                </Box>
              ) : null}
            </TableSortLabel>
          </TableCell>
        ))}
      </TableRow>
    </TableHead>
  );
}

function Patient({ isSignedIn }) {
  const [order, setOrder] = useState("asc");
  const [orderBy, setOrderBy] = useState("calories");
  const [page, setPage] = useState(0);
  const [rowsPerPage, setRowsPerPage] = useState(5);
  const [patients, setPatients] = useState([]);
  const [searchQuery, setSearchQuery] = useState("");
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

  useEffect(() => {
    if (!isSignedIn) {
      navigateTo("/SignIn");
    }
    getPatient();
  }, []);

  const getPatient = () => {
    fetchPatients()
      .then((response) => {
        setPatients([...response.data]);
      })
      .catch((error) => {
        console.error("Error fetching patients:", error.message);
      });
  };

  const handleRequestSort = (event, property) => {
    const isAsc = orderBy === property && order === "asc";
    setOrder(isAsc ? "desc" : "asc");
    setOrderBy(property);
  };

  const handleChangePage = (event, newPage) => {
    setPage(newPage);
  };

  const handleChangeRowsPerPage = (event) => {
    setRowsPerPage(parseInt(event.target.value, 10));
    setPage(0);
  };

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
          getPatient();
          alert("New Patient added ");
          handleCloseModal();
        })
        .catch((error) => {
          console.error("Error adding patient:", error.message);
          alert("Failed to add new patient ");
        });
    }
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

  const handleUpdatePatient = () => {
    const updatedPatient = {
      ...selectedPatient,
      ...newPatientData,
    };

    updatePatient(selectedPatient.id, updatedPatient)
      .then((response) => {
        console.log("Patient updated successfully:", response.data);
        handleCloseModal();
        getPatient();
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

  const handleDeletePatient = (id) => {
    const confirmDelete = window.confirm(
      "Are you sure you want to remove this patient ?"
    );
    if (confirmDelete) {
      deletePatient(id)
        .then((response) => {
          console.log(response);
          getPatient();
        })
        .catch((error) => {
          console.error("Error deleting patient:", error.message);
          alert("Failed to delete patient");
        });
    }
  };

  const filteredAppointments = useMemo(() => {
    return patients.filter((patient) =>
      patient.name.toLowerCase().includes(searchQuery.toLowerCase())
    );
  }, [patients, searchQuery]);

  const visibleRows = useMemo(
    () =>
      stableSort(filteredAppointments, getComparator(order, orderBy)).slice(
        page * rowsPerPage,
        page * rowsPerPage + rowsPerPage
      ),
    [filteredAppointments, order, orderBy, page, rowsPerPage]
  );

  const isSelected = (id) => false;

  return (
    <Box sx={{ width: "100%" }}>
      <Paper sx={{ width: "100%", mb: 2 }}>
        <Box
          sx={{
            display: "flex",
            justifyContent: "space-between",
            alignItems: "center",
            p: 2,
          }}
        >
          <TextField
            id="search"
            label="Search"
            value={searchQuery}
            onChange={(e) => setSearchQuery(e.target.value)}
          />
          <Typography variant="h5" id="tableTitle" component="div">
            Patient List
          </Typography>
          <Button
            variant="contained"
            onClick={handleOpenModal}
            style={{ padding: "15px 20px", width: "220px" }}
          >
            Add Patient
          </Button>
        </Box>
        <TableContainer>
          <Table
            sx={{ minWidth: 750 }}
            aria-labelledby="tableTitle"
            size="medium"
          >
            <EnhancedTableHead
              order={order}
              orderBy={orderBy}
              onRequestSort={handleRequestSort}
            />
            <TableBody>
              {visibleRows.map((patient, index) => {
                const labelId = `enhanced-table-checkbox-${index}`;

                return (
                  <TableRow
                    hover
                    role="checkbox"
                    tabIndex={-1}
                    key={patient.id}
                    selected={isSelected(patient.id)}
                    sx={{ cursor: "pointer" }}
                  >
                    <TableCell component="th" id={labelId} scope="patient">
                      {patient.name}
                    </TableCell>
                    <TableCell align="left">{patient.dateOfBirth}</TableCell>
                    <TableCell align="left">{patient.email}</TableCell>
                    <TableCell align="left">{patient.phoneNo}</TableCell>
                    <TableCell align="left">{patient.medicalHistory}</TableCell>
                    <TableCell align="left">
                      <IconButton
                        aria-label="update"
                        onClick={() => handleOpenUpdateModal(patient)}
                      >
                        <EditIcon />
                      </IconButton>
                      <IconButton
                        aria-label="delete"
                        onClick={() => handleDeletePatient(patient.id)}
                      >
                        <DeleteIcon />
                      </IconButton>
                    </TableCell>
                  </TableRow>
                );
              })}
            </TableBody>
          </Table>
        </TableContainer>
        <TablePagination
          rowsPerPageOptions={[5, 10, 25]}
          component="div"
          count={patients.length}
          rowsPerPage={rowsPerPage}
          page={page}
          onPageChange={handleChangePage}
          onRowsPerPageChange={handleChangeRowsPerPage}
        />
      </Paper>
      {/* Modal */}
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
            >
              {newPatientData.id ? "Update Patient" : "Add Patient"}
            </Button>
          </Box>
        </Box>
      </Modal>
    </Box>
  );
}

export default Patient;
