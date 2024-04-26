import React, { useState, useEffect, useMemo } from "react";
import { useNavigate } from "react-router-dom";
import axios from "axios";
import PropTypes from "prop-types";
import { alpha } from "@mui/material/styles";
import Box from "@mui/material/Box";
import Table from "@mui/material/Table";
import TableBody from "@mui/material/TableBody";
import TableCell from "@mui/material/TableCell";
import TableContainer from "@mui/material/TableContainer";
import TableHead from "@mui/material/TableHead";
import TablePagination from "@mui/material/TablePagination";
import TableRow from "@mui/material/TableRow";
import TableSortLabel from "@mui/material/TableSortLabel";
import Toolbar from "@mui/material/Toolbar";
import Typography from "@mui/material/Typography";
import Paper from "@mui/material/Paper";
import IconButton from "@mui/material/IconButton";
import CloseIcon from "@mui/icons-material/Close";
import Tooltip from "@mui/material/Tooltip";
import FormControlLabel from "@mui/material/FormControlLabel";
import FilterListIcon from "@mui/icons-material/FilterList";
import { visuallyHidden } from "@mui/utils";
import TextField from "@mui/material/TextField";
import Button from "@mui/material/Button";
import Modal from "@mui/material/Modal";
import EditIcon from "@mui/icons-material/Edit";
import DeleteIcon from "@mui/icons-material/Delete";
import Checkbox from "@mui/material/Checkbox";
import Autocomplete from "@mui/material/Autocomplete";
import {
  fetchAppointments,
  addAppointment,
  updateAppointment,
  deleteAppointment,
} from "../Api/AppointmentApi";
import { fetchPatients } from "../Api/PatientApi";

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
  {
    id: "id",
    numeric: false,
    disablePadding: true,
    label: "AppointmentID",
  },
  {
    id: "patientName",
    numeric: true,
    disablePadding: false,
    label: "Patient Name",
  },
  {
    id: "date",
    numeric: true,
    disablePadding: false,
    label: "Date",
  },
  {
    id: "time",
    numeric: true,
    disablePadding: false,
    label: "Time",
  },
  {
    id: "reason",
    numeric: true,
    disablePadding: false,
    label: "Reason",
  },
  {
    id: "instruction",
    numeric: true,
    disablePadding: false,
    label: "Instruction",
  },
  {
    id: "actions",
    numeric: false,
    disablePadding: false,
    label: "Actions",
  },
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

EnhancedTableHead.propTypes = {
  numSelected: PropTypes.number.isRequired,
  onRequestSort: PropTypes.func.isRequired,
  onSelectAllClick: PropTypes.func.isRequired,
  order: PropTypes.oneOf(["asc", "desc"]).isRequired,
  orderBy: PropTypes.string.isRequired,
  rowCount: PropTypes.number.isRequired,
};

function EnhancedTableToolbar(props) {
  const { numSelected } = props;

  return (
    <Toolbar
      sx={{
        pl: { sm: 2 },
        pr: { xs: 1, sm: 1 },
        ...(numSelected > 0 && {
          bgcolor: (theme) =>
            alpha(
              theme.palette.primary.main,
              theme.palette.action.activatedOpacity
            ),
        }),
      }}
    >
      {numSelected > 0 ? (
        <Typography
          sx={{ flex: "1 1 100%" }}
          color="inherit"
          variant="subtitle1"
          component="div"
        >
          {numSelected} selected
        </Typography>
      ) : (
        <Typography
          sx={{ flex: "1 1 100%" }}
          variant="h6"
          id="tableTitle"
          component="div"
        >
          Appointment List
        </Typography>
      )}

      {numSelected > 0 ? (
        <Tooltip title="Delete">
          <IconButton>
            <DeleteIcon />
          </IconButton>
        </Tooltip>
      ) : (
        <Tooltip title="Filter list">
          <IconButton>
            <FilterListIcon />
          </IconButton>
        </Tooltip>
      )}
    </Toolbar>
  );
}

EnhancedTableToolbar.propTypes = {
  numSelected: PropTypes.number.isRequired,
};

export default function Appointment({ isSignedIn }) {
  const [order, setOrder] = useState("asc");
  const [orderBy, setOrderBy] = useState("id");
  const [selected, setSelected] = useState([]);
  const [page, setPage] = useState(0);
  const [dense, setDense] = useState(false);
  const [rowsPerPage, setRowsPerPage] = useState(5);
  const [appointments, setAppointments] = useState([]);
  const [searchQuery, setSearchQuery] = useState("");
  const [openModal, setOpenModal] = useState(false);
  const [newAppointmentData, setNewAppointmentData] = useState({
    patientName: "",
    date: "",
    time: "",
    reason: "",
    instruction: "",
  });
  const [selectedAppointment, setSelectedAppointment] = useState(null);
  const [showUpcoming, setShowUpcoming] = useState(false);
  const [patientsList, setPatientsList] = useState([]);
  const [selectedPatient, setSelectedPatient] = useState("");
  const [errorMessage, setErrorMessage] = useState("");
  const navigateTo = useNavigate();

  useEffect(() => {
    console.log(isSignedIn);
    if (!isSignedIn) {
      navigateTo("/SignIn");
    }
    getPatientsList();
  }, []);

  const getPatientsList = () => {
    fetchPatients()
      .then((response) => {
        console.log("Patients List:", response.data);
        setPatientsList(response.data);
      })
      .catch((error) => {
        console.error("Error fetching patients list:", error.message);
      });
  };

  const handleCheckboxChange = () => {
    setShowUpcoming(!showUpcoming);
  };

  const getAppointments = () => {
    fetchAppointments()
      .then((response) => {
        console.log("Response:", response.data);
        setAppointments([...response.data]);
      })
      .catch((error) => {
        console.error("Error fetching appointments:", error.message);
      });
  };

  useEffect(() => {
    console.log("fetching appointment details");
    getAppointments();
  }, []);

  const handleRequestSort = (event, property) => {
    const isAsc = orderBy === property && order === "asc";
    setOrder(isAsc ? "desc" : "asc");
    setOrderBy(property);
  };

  const handleSelectAllClick = (event) => {
    if (event.target.checked) {
      const newSelected = appointments.map((n) => n.id);
      setSelected(newSelected);
      return;
    }
    setSelected([]);
  };

  const handleChangePage = (event, newPage) => {
    setPage(newPage);
  };

  const handleChangeRowsPerPage = (event) => {
    setRowsPerPage(parseInt(event.target.value, 10));
    setPage(0);
  };

  const isSelected = (id) => selected.indexOf(id) !== -1;

  const filteredAppointments = useMemo(() => {
    return appointments.filter((appointment) => {
      // filter by name
      const matchesSearch = appointment.patientName
        .toLowerCase()
        .includes(searchQuery.toLowerCase());

      // filter by date (upcoming appointments)
      const appointmentDateTime = new Date(
        `${appointment.date} ${appointment.time}`
      );
      const isUpcoming = appointmentDateTime > new Date();

      return matchesSearch && (!showUpcoming || isUpcoming);
    });
  }, [appointments, searchQuery, showUpcoming]);

  const visibleRows = useMemo(
    () =>
      stableSort(filteredAppointments, getComparator(order, orderBy)).slice(
        page * rowsPerPage,
        page * rowsPerPage + rowsPerPage
      ),
    [filteredAppointments, order, orderBy, page, rowsPerPage]
  );

  const emptyRows =
    page > 0
      ? Math.max(0, (1 + page) * rowsPerPage - filteredAppointments.length)
      : 0;

  const handleOpenModal = () => {
    setOpenModal(true);
  };

  const handleCloseModal = () => {
    setNewAppointmentData({
      patientId: "",
      patientName: "",
      date: "",
      time: "",
      reason: "",
      instruction: "",
    });
    setSelectedAppointment(null);
    setErrorMessage("");
    setOpenModal(false);
  };

  const handleOpenUpdateModal = (appointment) => {
    setSelectedAppointment(appointment);

    setNewAppointmentData({
      ...appointment,
    });
    console.log(newAppointmentData);
    setOpenModal(true);
  };

  const handleInputChange = (name, value) => {
    setNewAppointmentData((prevData) => ({
      ...prevData,
      [name]: value,
    }));
  };

  const handleAddAppointment = () => {
    if (validateForm()) {
      let id;
      if (appointments.length === 0) {
        id = 1;
      } else {
        id = parseInt(appointments[appointments.length - 1].id) + 1;
        id = id.toString();
      }
      addAppointment({
        id: id,
        ...newAppointmentData,
        patientId: selectedPatient.id,
        patientName: selectedPatient.name,
      })
        .then((response) => {
          console.log("New appointment added successfully:", response.data);
          handleCloseModal();
          getAppointments();
          alert("Appointment Added ");
        })
        .catch((error) => {
          console.error("Error adding new appointment:", error.message);
          alert("Failed to save appointment");
        });
    }
  };

  const validateForm = () => {
    const errors = {};
    if (newAppointmentData.date === "") {
      errors.date = "Date is required";
    }
    if (newAppointmentData.time === "") {
      errors.time = "Time is required";
    }
    if (newAppointmentData.reason === "") {
      errors.reason = "Reason is required";
    }
    if (selectedPatient === "") {
      errors.patientName = "Patient name is required";
    }
    setErrorMessage({ ...errors });

    return Object.keys(errors).length < 1;
  };

  const handleUpdateAppointment = () => {
    const updatedAppointment = {
      ...selectedAppointment,
      ...newAppointmentData,
    };

    updateAppointment(selectedAppointment.id, updatedAppointment)
      .then((response) => {
        console.log("Appointment updated successfully:", response.data);
        handleCloseModal();
        getAppointments();
        alert("Appointment Updated ! ");
      })
      .catch((error) => {
        console.error("Error updating appointment:", error.message);
        alert("Failed to update appointment");
      });
  };

  const handleDeleteAppointment = (id) => {
    const confirmDelete = window.confirm(
      "Are you sure you want to remove this appointment?"
    );
    if (confirmDelete) {
      deleteAppointment(id)
        .then((response) => {
          console.log("Appointment removed successfully:", id);
          getAppointments();
        })
        .catch((error) => {
          console.error("Error deleting appointment:", error.message);
          alert("Failed to remove appointment");
        });
    }
  };

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
            Appointment List
          </Typography>
          <Button
            variant="contained"
            onClick={handleOpenModal}
            style={{ padding: "15px 20px" }}
          >
            Add Appointment
          </Button>
        </Box>
        <FormControlLabel
          control={
            <Checkbox
              checked={showUpcoming}
              onChange={handleCheckboxChange}
              color="primary"
            />
          }
          style={{ marginLeft: "10px" }}
          label="Show Upcoming Appointments"
        />
        <TableContainer>
          <Table
            sx={{ minWidth: 750, padding: "20px" }}
            aria-labelledby="tableTitle"
            size={dense ? "small" : "medium"}
          >
            <EnhancedTableHead
              numSelected={selected.length}
              order={order}
              orderBy={orderBy}
              onSelectAllClick={handleSelectAllClick}
              onRequestSort={handleRequestSort}
              rowCount={filteredAppointments.length}
            />
            <TableBody>
              {visibleRows.map((appointment, index) => {
                const isItemSelected = isSelected(appointment.id);
                const labelId = `enhanced-table-checkbox-${index}`;

                return (
                  <TableRow
                    hover
                    role="checkbox"
                    aria-checked={isItemSelected}
                    tabIndex={-1}
                    key={appointment.id}
                    selected={isItemSelected}
                    sx={{ cursor: "pointer" }}
                  >
                    <TableCell
                      component="th"
                      id={labelId}
                      scope="appointment"
                      padding="normal"
                    >
                      {appointment.id}
                    </TableCell>
                    <TableCell align="left">
                      {appointment.patientName}
                    </TableCell>
                    <TableCell align="left">{appointment.date}</TableCell>
                    <TableCell align="left">{appointment.time}</TableCell>
                    <TableCell align="left">{appointment.reason}</TableCell>
                    <TableCell align="left">
                      {appointment.instruction}
                    </TableCell>
                    <TableCell align="left">
                      <IconButton
                        onClick={() => handleOpenUpdateModal(appointment)}
                      >
                        <EditIcon />
                      </IconButton>
                      <IconButton
                        onClick={() => handleDeleteAppointment(appointment.id)}
                      >
                        <DeleteIcon />
                      </IconButton>
                    </TableCell>
                  </TableRow>
                );
              })}
              {emptyRows > 0 && (
                <TableRow
                  style={{
                    height: (dense ? 33 : 53) * emptyRows,
                  }}
                >
                  <TableCell colSpan={7} />
                </TableRow>
              )}
            </TableBody>
          </Table>
        </TableContainer>
        <TablePagination
          rowsPerPageOptions={[5, 10, 25]}
          component="div"
          count={filteredAppointments.length}
          rowsPerPage={rowsPerPage}
          page={page}
          onPageChange={handleChangePage}
          onRowsPerPageChange={handleChangeRowsPerPage}
        />
      </Paper>

      <Modal
        open={openModal}
        onClose={handleCloseModal}
        aria-labelledby="add-appointment-modal"
        aria-describedby="add-appointment-form"
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
              {selectedAppointment
                ? "Update Appointment"
                : "Add New Appointment"}
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
          {selectedAppointment ? (
            ""
          ) : (
            <Box sx={{ marginBottom: 2 }}>
              <Autocomplete
                id="patientName"
                name="patientName"
                options={[...patientsList, { name: "" }]}
                getOptionLabel={(option) => (option.name ? option.name : "")}
                value={selectedPatient}
                onChange={(event, newValue) => {
                  setSelectedPatient(newValue);
                }}
                renderInput={(params) => (
                  <TextField {...params} label="Patient Name" fullWidth />
                )}
              />
              <div className="errorMessage">{errorMessage["patientName"]}</div>
            </Box>
          )}

          <Box sx={{ marginBottom: 2 }}>
            <TextField
              id="date"
              name="date"
              type="date"
              label="Date"
              InputLabelProps={{
                shrink: true,
                style: { width: "100px" },
              }}
              value={newAppointmentData.date}
              onChange={(e) => handleInputChange("date", e.target.value)}
              fullWidth
            />
            <div className="errorMessage">{errorMessage["date"]}</div>
          </Box>

          <Box sx={{ marginBottom: 2 }}>
            <TextField
              id="time"
              name="time"
              type="time"
              label="Time"
              InputLabelProps={{
                shrink: true,
                style: { width: "100px" },
              }}
              value={newAppointmentData.time}
              onChange={(e) => handleInputChange("time", e.target.value)}
              fullWidth
            />
            <div className="errorMessage">{errorMessage["time"]}</div>
          </Box>

          <Box sx={{ marginBottom: 2 }}>
            <TextField
              id="reason"
              name="reason"
              label="Reason"
              value={newAppointmentData.reason}
              onChange={(e) => handleInputChange("reason", e.target.value)}
              fullWidth
            />
            <div className="errorMessage">{errorMessage["reason"]}</div>
          </Box>

          <TextField
            id="instruction"
            name="instruction"
            label="Instruction"
            value={newAppointmentData.instruction}
            onChange={(e) => handleInputChange("instruction", e.target.value)}
            fullWidth
            sx={{ marginBottom: 2 }}
          />
          <Box sx={{ display: "flex", justifyContent: "end" }}>
            <Button
              variant="contained"
              onClick={
                selectedAppointment
                  ? handleUpdateAppointment
                  : handleAddAppointment
              }
            >
              {selectedAppointment ? "Update Appointment" : "Add Appointment"}
            </Button>
          </Box>
        </Box>
      </Modal>
    </Box>
  );
}
