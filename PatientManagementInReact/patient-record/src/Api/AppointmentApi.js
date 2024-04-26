import axios from "axios";

const baseURL = "http://localhost:3000/appointments";

export const fetchAppointments = () => {
  return axios.get(`${baseURL}`);
};

export const addAppointment = (data) => {
  return axios.post(`${baseURL}`, data);
};

export const updateAppointment = (id, data) => {
  return axios.put(`${baseURL}/${id}`, data);
};

export const deleteAppointment = (id) => {
  return axios.delete(`${baseURL}/${id}`);
};
