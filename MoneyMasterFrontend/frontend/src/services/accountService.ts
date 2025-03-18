import api from "../api/api";

export const accountService = {
  getAccounts: async (accessToken: string) => {
    try {
      const response = await api.get("/accounts", {
        headers: { Authorization: `Bearer ${accessToken}` },
      });
      return response.data;
    } catch (error) {
      console.error("Ошибка при получении счетов:", error);
      throw error;
    }
  },

  getCreateInfo: async (accessToken: string) => {
    try {
      const response = await api.get("/accounts/create", {
        headers: { Authorization: `Bearer ${accessToken}` },
      });
      console.log(response);
      return response.data;
    } catch (error) {
      console.error(
        "Ошибка при получении инофрмации для создания счета:",
        error
      );
      throw error;
    }
  },

  createAccount: async (
    accessToken: string,
    userId: string | undefined,
    name: string,
    balance: number,
    accountTypeId: string | undefined,
    currency: string
  ) => {
    try {
      console.log({ userId, name, balance, accountTypeId, icon: "", currency });
      const response = await api.post(
        "/accounts",
        {
          userId,
          name,
          balance,
          accountTypeId,
          icon: "string",
          currency,
          createAt: "2025-03-04T12:43:03.438Z",
        },
        { headers: { Authorization: `Bearer ${accessToken}` } }
      );
      return response.data;
    } catch (error) {
      console.error("Ошибка при создани счета:", error);
      throw error;
    }
  },

  deleteAccount: async (accessToken: string, accountId: string) => {
    await api.delete(`/accounts/${accountId}`, {
      headers: { Authorization: `Bearer ${accessToken}` },
    });
  },
};
