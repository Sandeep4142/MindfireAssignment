import { useEffect, useState } from "react";
import axios from "axios";
import {
  IconButton,
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TablePagination,
  TableRow,
  TableSortLabel,
} from "@mui/material";
import { Delete, Edit } from "@mui/icons-material";
import { fetchPatients, deletePatient } from "../../Api/PatientApi";

function PatientTable({ setOpenModal,patientList,setPatientList, setNewPatientData }) {
  const [pageNumber, setPageNumber] = useState(0);
  const [pageLength, setPageLength] = useState(3);
  const [totalCount, setTotalCount] = useState(0);
  const [sortBy, setSortBy] = useState("name");
  const [sortOrder, setSortOrder] = useState("asc");

  const columns = [
    { id: "name", name: "Name" },
    { id: "dateOfBirth", name: "Date Of Birth" },
    { id: "email", name: "Email" },
    { id: "phoneNo", name: "Phone No" },
    { id: "medicalHistory", name: "Medical History" },
    { id: "actions", name: "Actions" },
  ];

  const handleEdit = (patientId) => {
    const patient = patientList.find((patient) => patient.id === patientId);
    setNewPatientData(patient);
    setOpenModal(true);
  };

  const handleDelete = (patientId) => {
    if (window.confirm("Are you sure you want to delete this patient?")) {
      deletePatient(patientId)
        .then((response) => {
          setPatientList((prevList) =>
            prevList.filter((patient) => patient.id !== patientId)
          );
          getSortedPagedPatientList();
        })
        .catch((error) => {
          console.error("Error deleting patient:", error.message);
          alert("Failed to remove appointment");
        });
    }
  };

  const handleSort = (columnId) => {
    const newSortOrder =
      sortBy === columnId && sortOrder === "asc" ? "desc" : "asc";
    setSortBy(columnId);
    setSortOrder(newSortOrder);
  };

  const getSortedPagedPatientList = async () => {
    const start = pageNumber * pageLength;
    const end = start + pageLength;
    const sortParam = sortOrder === "asc" ? sortBy : "-" + sortBy;

    const response = await axios.get(
      `http://localhost:3000/patients?_sort=${sortParam}&_start=${start}&_end=${end}`
    );
    const totalPatients = await fetchPatients();

    setTotalCount(totalPatients.data.length);
    setPatientList(response.data);
  };

  useEffect(() => {
    getSortedPagedPatientList();
  }, [pageNumber, pageLength, sortBy, sortOrder]);

  return (
    <>
      <TableContainer sx={{ maxHeight: 450 }}>
        <Table stickyHeader>
          <TableHead>
            <TableRow>
              {columns.map((column) => (
                <TableCell key={column.id}>
                  <TableSortLabel
                    active={sortBy === column.id}
                    direction={sortBy === column.id ? sortOrder : "asc"}
                    onClick={() => handleSort(column.id)}
                    className="tableHead"
                  >
                    {column.name}
                  </TableSortLabel>
                </TableCell>
              ))}
            </TableRow>
          </TableHead>
          <TableBody>
            {patientList.length > 0 ? (
              patientList.map((patient) => (
                <TableRow key={patient.id}>
                  {columns.map((column) => (
                    <TableCell key={column.id}>
                      {column.id === "actions" ? (
                        <>
                          <IconButton onClick={() => handleEdit(patient.id)}>
                            <Edit />
                          </IconButton>
                          <IconButton onClick={() => handleDelete(patient.id)}>
                            <Delete />
                          </IconButton>
                        </>
                      ) : (
                        patient[column.id]
                      )}
                    </TableCell>
                  ))}
                </TableRow>
              ))
            ) : (
              <TableRow>
                <TableCell colSpan={columns.length} align="center">
                  No patient found
                </TableCell>
              </TableRow>
            )}
          </TableBody>
        </Table>
      </TableContainer>
      {patientList.length > 0 && (
        <TablePagination
          rowsPerPageOptions={[3, 5, 10, 20]}
          page={pageNumber}
          rowsPerPage={pageLength}
          count={totalCount}
          component="div"
          onPageChange={(event, newPage) => setPageNumber(newPage)}
          onRowsPerPageChange={(event) => {
            setPageLength(parseInt(event.target.value, 10));
            setPageNumber(0);
          }}
        />
      )}
    </>
  );
}

export default PatientTable;
