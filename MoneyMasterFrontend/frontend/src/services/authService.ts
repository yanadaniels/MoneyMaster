import { User } from "@/types/user";
import axios from "axios";

const API_URL = "https://localhost:21001/api/v1/users";

export const authService = {
  login: async (email: string, password: string, dispatch: any) => {
    const response = await axios.post(`${API_URL}/login`, {
      email: email,
      password,
    });
    localStorage.setItem("accsessToken", response.data.accsessToken);
    localStorage.setItem("refreshToken", response.data.refreshToken);
    localStorage.setItem("userId", JSON.stringify(response.data.id));
    dispatch({ type: "LOGIN", payload: response.data });
  },

  logout: async (dispatch: any) => {
    //await axios.post(`${API_URL}`, null);
    localStorage.removeItem("accsessToken");
    localStorage.removeItem("refreshToken");
    localStorage.removeItem("userId");
    dispatch({ type: "LOGOUT" });
  },

  registration: async (userName: string, email: string, password: string) => {
    await axios.post(`${API_URL}`, { userName, email, password });
  },

  getUserById: async (userId : string): Promise<User | null> => {
    try {
      console.log(`${API_URL}/${userId}`);
      const accessToken = authService.getToken();
      if (!accessToken) return null;

      const response = await axios.get(`${API_URL}/${userId}`, {
          headers: {
            Authorization: `Bearer ${accessToken}`,
          },
      });

      return response.data;
    } catch (error) {
      console.error("Ошибка при получении пользователя по ID", error);
      throw error;
    }
  },

  getToken: () => {
    return localStorage.getItem("accsessToken");
  },
};
