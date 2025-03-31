import axios from "axios";

const api = axios.create({
  baseURL: "https://localhost:20001/api/v1",
  headers: {
    "Context-Type": "application/json",
  },
});

export default api;
