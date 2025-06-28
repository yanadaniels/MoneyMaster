import { AccountResponse, UpdatingAccountRequest } from "@/types";
import api from "../api/api";
import { authService } from "@/services/authService";

export const accountService = {
  getAccounts: async () => {
    try {
      const accessToken = authService.getToken();
      if (!accessToken) return;

      const response = await api.get("/accounts", {
        headers: { Authorization: `Bearer ${accessToken}` },
      });
      return response.data;
    } catch (error) {
      console.error("Ошибка при получении счетов:", error);
      throw error;
    }
  },

  getCreateInfo: async () => {
    try {
      const accessToken = authService.getToken();
      if (!accessToken) return;

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
    userId: string | undefined,
    name: string,
    balance: number,
    accountTypeId: string | undefined,
    currency: string,
    icon: string
  ) => {
    try {
      const accessToken = authService.getToken();
      if (!accessToken) return;

      console.log({ userId, name, balance, accountTypeId, icon, currency });
      const response = await api.post(
        "/accounts",
        {
          userId,
          name,
          balance,
          accountTypeId,
          icon,
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

  getAccountById: async (
    accountId: string
  ): Promise<AccountResponse | null> => {
    try {
      const accessToken = authService.getToken();
      if (!accessToken) return null;

      const response = await api.get<AccountResponse>(
        `/accounts/${accountId}`,
        {
          headers: {
            Authorization: `Bearer ${accessToken}`,
          },
        }
      );
      return response.data;
    } catch (error) {
      console.error("Ошибка при получении транзакции по ID:", error);
      throw error;
    }
  },

  deleteAccount: async (accountId: string) => {
    const accessToken = authService.getToken();
    if (!accessToken) return;

    await api.delete(`/accounts/${accountId}`, {
      headers: { Authorization: `Bearer ${accessToken}` },
    });
  },

  updateAccount: async (updatedAccount: UpdatingAccountRequest): Promise<AccountResponse | null> => {
    try {
      const accessToken = authService.getToken();
      if (!accessToken) return null;

      const response = await api.put(
        `/accounts/${updatedAccount.id}`, updatedAccount,
        { headers: { Authorization: `Bearer ${accessToken}` } }
      );
      return response.data;
    } catch (error) {
      console.error("Ошибка при обновлении счета:", error);
      throw error;
    }
  }

};
