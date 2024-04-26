import axios from "axios";

const baseURL = "http://localhost:3000/patients";

export const fetchPatients = () => {
  return axios.get(`${baseURL}`);
};

export const addPatient = (data) => {
  return axios.post(`${baseURL}`, data);
};

export const updatePatient = (id, data) => {
  return axios.put(`${baseURL}/${id}`, data);
};

export const deletePatient = (id) => {
  return axios.delete(`${baseURL}/${id}`);
};
