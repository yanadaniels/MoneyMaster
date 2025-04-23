import axios from "axios";

const API_URL = "https://localhost:20001/api/v1/users";

export const authService = {
  login: async (email: string, password: string, dispatch: any) => {
    const response = await axios.post(`${API_URL}/login`, {
      userName: email,
      password,
    });
    console.log(response);
    localStorage.setItem("accsessToken", response.data.accsessToken);
    localStorage.setItem("refreshToken", response.data.refreshToken);
    dispatch({ type: "LOGIN", payload: response.data });
  },

  logout: async (dispatch: any) => {
    //await axios.post(`${API_URL}`, null);
    localStorage.removeItem("accsessToken");
    localStorage.removeItem("refreshToken");
    dispatch({ type: "LOGOUT" });
  },

  registration: async (userName: string, email: string, password: string) => {
    await axios.post(`${API_URL}`, { userName, email, password });
  },

  getToken: () => {
    return localStorage.getItem("accsessToken");
  },
};
