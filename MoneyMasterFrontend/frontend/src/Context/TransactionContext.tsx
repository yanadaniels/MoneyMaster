// TransactionContext.tsx
import React, {
  createContext,
  useContext,
  useState,
  ReactNode,
  useCallback,
  useMemo
} from "react";
import { TransactionResponse } from "@/types";
import { transactionService } from "@/services/transactionService";

interface TransactionContextType {
  transactions: TransactionResponse[];
  loading: boolean;
  error: string | null;
  addTransaction: (transaction: TransactionResponse) => void;
  updateTransaction: (updatedTransaction: TransactionResponse) => void;
  deleteTransaction: (id: string) => void;
  selectTransaction: (transaction: TransactionResponse | null) => void;
  clearSelectedTransaction: () => void;
  fetchTransactions: (accountId: string) => Promise<void>;
  selectedTransaction: TransactionResponse | null;
}

const TransactionContext = createContext<TransactionContextType | undefined>(undefined);

export const useTransactionContext = () => {
  const context = useContext(TransactionContext);
  if (!context) {
    throw new Error("useTransactionContext must be used within a TransactionProvider");
  }
  return context;
};

interface TransactionProviderProps {
  children: ReactNode;
}

export const TransactionProvider: React.FC<TransactionProviderProps> = ({
  children
}) => {
  const [transactions, setTransactions] = useState<TransactionResponse[]>([]);
  const [selectedTransaction, setSelectedTransaction] = useState<TransactionResponse | null>(null);
  const [loading, setLoading] = useState<boolean>(false);
  const [error, setError] = useState<string | null>(null);

  // Метод для загрузки транзакций по accountId
  const fetchTransactions = useCallback(async (accountId: string) => {
    try {
      setLoading(true);
      setError(null);
      const response = await transactionService.getTransactionByAccountId(accountId);
      if (response) setTransactions(response);
    } catch (err) {
      setError(err instanceof Error ? err.message : "Ошибка при загрузке транзакций");
      console.error("Failed to fetch transactions:", err);
    } finally {
      setLoading(false);
    }
  }, []);

  // Добавление транзакции
  const addTransaction = useCallback((transaction: TransactionResponse) => {
    setTransactions(prev => [...prev, transaction]);
  }, []);

  // Обновление транзакции
  const updateTransaction = useCallback((updatedTransaction: TransactionResponse) => {
    setTransactions(prev =>
      prev.map(transaction =>
        transaction.id === updatedTransaction.id ? updatedTransaction : transaction
      )
    );
  }, []);

  // Удаление транзакции
  const deleteTransaction = useCallback((id: string) => {
    setTransactions(prev => prev.filter(transaction => transaction.id !== id));
  }, []);

  // Выбор транзакции
  const selectTransaction = useCallback((transaction: TransactionResponse | null) => {
    setSelectedTransaction(transaction);
  }, []);

  // Сброс выбранной транзакции
  const clearSelectedTransaction = useCallback(() => {
    setSelectedTransaction(null);
  }, []);

  // Мемоизация значения контекста
  const contextValue = useMemo(() => ({
    transactions,
    loading,
    error,
    addTransaction,
    updateTransaction,
    deleteTransaction,
    selectTransaction,
    clearSelectedTransaction,
    fetchTransactions,
    selectedTransaction
  }), [
    transactions,
    loading,
    error,
    addTransaction,
    updateTransaction,
    deleteTransaction,
    selectTransaction,
    clearSelectedTransaction,
    fetchTransactions,
    selectedTransaction
  ]);

  return (
    <TransactionContext.Provider value={contextValue}>
      {children}
    </TransactionContext.Provider>
  );
};