import api from "../api/api";

export const transactionService = {
  getTransactions: async (accessToken: string) => {
    try {
      const response = await api.get("/transactions", {
        headers: {
          Authorization: `Bearer ${accessToken}`,
        },
      });
      return response.data;
    } catch (error) {
      console.error("Ошибка при получении транзакций:", error);
      throw error;
    }
  },
};
