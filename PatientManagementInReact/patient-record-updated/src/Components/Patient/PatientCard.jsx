import { useEffect, useState } from "react";
import axios from "axios";
import { IconButton, Grid, Card, ThemeProvider, CardContent, Typography } from "@mui/material";
import { Delete, Edit } from "@mui/icons-material";
import { createTheme } from '@mui/material/styles';

const theme = createTheme({
  palette: {
    cardColor: '#f1eff5', 
  },
});

function PatientCard({
  patientList,
  setPatientList,
  setNewPatientData,
  setOpenModal,
  handleDeletePatient
}) {
  const [editBtnClicked, setEditBtnClicked] = useState(false);
  const [pageNumber, setPageNumber] = useState(1);
  const [totalPage, setTotalPage] = useState(1);
  const [editId, setEditId] = useState(0);

  const handleEdit = (patientId) => {
    setEditId(patientId);
    setEditBtnClicked(true);
  };

  useEffect(() => {
    if (editBtnClicked) {
      const patient = patientList.find((patient) => patient.id === editId);
      if (patient) {
        setNewPatientData(patient);
        setOpenModal(true);
      }
      setEditBtnClicked(false);
    }
  }, [editBtnClicked]);

  //////////////// infinite scrolling

  const getCardData = async () => {
    const res = await axios.get(
      `http://localhost:3000/patients?_per_page=9&_page=${pageNumber}&_sort=name`
    );
    setTotalPage(res.data.pages);
    const data = res.data.data;
    console.log(pageNumber);
    console.log(data);
    setPatientList((prev) => [...prev, ...data]);
  };

  useEffect(() => {
    if (pageNumber <= totalPage) {
      getCardData();
    }
  }, [pageNumber]);

  const handelInfiniteScroll = async () => {
    try {
      if (
        window.innerHeight + document.documentElement.scrollTop + 1 >=
        document.documentElement.scrollHeight
      ) {
        setPageNumber((prev) => prev + 1);
      }
    } catch (error) {
      console.log(error);
    }
  };

  useEffect(() => {
    window.addEventListener("scroll", handelInfiniteScroll);
    return () => window.removeEventListener("scroll", handelInfiniteScroll);
  }, []);

  ////////////////

  return (
    <ThemeProvider theme={theme}>
      <Grid container spacing={2}>
        {patientList.map((patient, index) => (
          <Grid
            item
            xs={12}
            sm={6}
            md={4}
            lg={3}
            key={`${patient.id}-${index}`}
          >
            <Card sx={{ backgroundColor: 'cardColor' }}>
              <CardContent>
                <Typography variant="h5" component="div">
                  {patient.name}
                </Typography>
                <Typography variant="body2" color="text.secondary">
                  Date of Birth: {patient.dateOfBirth}
                </Typography>
                <Typography variant="body2" color="text.secondary">
                  Email: {patient.email}
                </Typography>
                <Typography variant="body2" color="text.secondary">
                  Phone No: {patient.phoneNo}
                </Typography>
                <Typography variant="body2" color="text.secondary">
                  Medical History: {patient.medicalHistory || "None"}
                </Typography>
                <div
                  style={{
                    display: "flex",
                    justifyContent: "flex-end",
                    marginTop: "5px",
                  }}
                >
                  <IconButton onClick={() => handleEdit(patient.id)}>
                    <Edit />
                  </IconButton>
                  <IconButton onClick={() => handleDeletePatient(patient.id)}>
                    <Delete />
                  </IconButton>
                </div>
              </CardContent>
            </Card>
          </Grid>
        ))}
      </Grid>
      </ThemeProvider>
  );
}

export default PatientCard;
