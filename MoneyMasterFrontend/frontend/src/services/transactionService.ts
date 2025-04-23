import {
  CreateTransactionRequest,
  TransactionResponse,
  CreateTransactionTransferRequest,
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

      console.log(payload);
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
};
