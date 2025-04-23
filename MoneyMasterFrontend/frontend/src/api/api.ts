import axios from "axios";

const api = axios.create({
  baseURL: "https://localhost:21001/api/v1",
  headers: {
    "Context-Type": "application/json",
  },
});

export default api;
