import {
  CreateTransactionRequest,
  TransactionResponse,
  CreateTransactionTransferRequest,
  UpdateTransactionRequest
} from "@/types";
import api from "../api/api";
import { authService } from "@/services/authService";

export const transactionService = {
  getTransactions: async (): Promise<TransactionResponse[]> => {
    try {
      const accessToken = authService.getToken();
      if (!accessToken) return [];

      const response = await api.get<TransactionResponse[]>("/transactions", {
        headers: {
          Authorization: `Bearer ${accessToken}`,
        },
      });
      return response.data;
    } catch (error) {
      console.error("Ошибка при получении всех транзакции:", error);
      throw error;
    }
  },

  createTransaction: async (
    payload: CreateTransactionRequest
  ): Promise<string | null> => {
    try {
      const accessToken = authService.getToken();
      if (!accessToken) return null;

      const response = await api.post<string>("/transactions", payload, {
        headers: { Authorization: `Bearer ${accessToken}` },
      });
      return response.data;
    } catch (error) {
      console.error("Ошибка при создани транзакции:", error);
      throw error;
    }
  },

  createTransactionTransfer: async (
    payload: CreateTransactionTransferRequest
  ): Promise<string | null> => {
    try {
      const accessToken = authService.getToken();
      if (!accessToken) return null;

      console.log(payload);
      const response = await api.post<string>(
        "/transactions/transfer",
        payload,
        {
          headers: { Authorization: `Bearer ${accessToken}` },
        }
      );
      return response.data;
    } catch (error) {
      console.error(
        "Ошибка при создани транзакции для перевода между счетами:",
        error
      );
      throw error;
    }
  },

  updateTransaction: async (
    transactionId: string,
    payload: UpdateTransactionRequest
  ): Promise<TransactionResponse | null> => {
    try {
      const accessToken = authService.getToken();
      if (!accessToken) return null;

      const response = await api.put<TransactionResponse>(`/transactions/${transactionId}`, payload, {
        headers: { Authorization: `Bearer ${accessToken}` },
      });
      return response.data;
    } catch (error) {
      console.error("Ошибка при изменении транзакции:", error);
      throw error;
    }
  },

  getTransactionById: async (
    transactionId: string
  ): Promise<TransactionResponse | null> => {
    try {
      const accessToken = authService.getToken();
      if (!accessToken) return null;

      const response = await api.get<TransactionResponse>(
        `/transactions/${transactionId}`,
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

  getTransactionByAccountId: async (
    transactionId: string
  ): Promise<TransactionResponse[] | null> => {
    try {
      const accessToken = authService.getToken();
      if (!accessToken) return null;

      const response = await api.get<TransactionResponse[]>(
        `/transactions/account/${transactionId}`,
        {
          headers: {
            Authorization: `Bearer ${accessToken}`,
          },
        }
      );
      return response.data;
    } catch (error) {
      console.error("Ошибка при получении транзакции по ID счета:", error);
      throw error;
    }
  },

  getFilteredTransaction: async (
    accountId: string,
    startDate: Date | null,
    endDate: Date | null
  ): Promise<TransactionResponse[] | null> => {
    try {
      const accessToken = authService.getToken();
      if (!accessToken) return null;

      // Проверка обязательных параметров
      if (!startDate || !endDate) {
        console.warn("Не указаны даты для фильтрации");
        return null;
      }

      const response = await api.get<TransactionResponse[]>(
        `/transactions/GetByDataRange/${accountId}`,
        {
          params: {
            startDate: startDate.toISOString(),
            endDate: endDate.toISOString()
          },
          headers: {
            Authorization: `Bearer ${accessToken}`,
          },
        }
      );

      return response.data;
    } catch (error) {
      console.error("Ошибка при получении отфильтрованных транзакций по ID счета:", error);
      throw error;
    }
  },

  deleteTransactionById: async (
    transactionId: string
  ): Promise<TransactionResponse | null> => {
    try {
      const accessToken = authService.getToken();
      if (!accessToken) return null;

      const response = await api.delete<TransactionResponse>(
        `/transactions/${transactionId}`,
        {
          headers: {
            Authorization: `Bearer ${accessToken}`,
          },
        }
      );
      return response.data;
    } catch (error) {
      console.error("Ошибка при удалении транзакции по ID:", error);
      throw error;
    }
  },

  
};
