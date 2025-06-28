// AccountContext.tsx
import React, {
  createContext,
  useContext,
  useState,
  useEffect,
  ReactNode,
  useCallback,
  useMemo
} from "react";
import { AccountResponse } from "@/types";
import { accountService } from "@/services/accountService"; // Предполагается, что у вас есть такой сервис

interface AccountContextType {
  accounts: AccountResponse[];
  loading: boolean;
  error: string | null;
  addAccount: (account: AccountResponse) => void;
  updateAccount: (updatedAccount: AccountResponse) => void;
  deleteAccount: (id: string) => void;
  selectAccount: (account: AccountResponse | null) => void;
  clearSelectedAccount: () => void;
  fetchAccounts: () => Promise<void>;
  getAccountById: (id: string) => AccountResponse | undefined;
  selectedAccount: AccountResponse | null;
}

const AccountContext = createContext<AccountContextType | undefined>(undefined);

export const useAccountContext = () => {
  const context = useContext(AccountContext);
  if (!context) {
    throw new Error("useAccountContext must be used within an AccountProvider");
  }
  return context;
};

interface AccountProviderProps {
  children: ReactNode;
  initialAccounts?: AccountResponse[];
}

export const AccountProvider: React.FC<AccountProviderProps> = ({
  children,
  initialAccounts = []
}) => {
  const [accounts, setAccounts] = useState<AccountResponse[]>(initialAccounts);
  const [selectedAccount, setSelectedAccount] = useState<AccountResponse | null>(null);
  const [loading, setLoading] = useState<boolean>(false);
  const [error, setError] = useState<string | null>(null);

  // Загрузка счетов при монтировании
  const fetchAccounts = useCallback(async () => {
    try {
      setLoading(true);
      setError(null);
      const response = await accountService.getAccounts();
      setAccounts(response.data);
    } catch (err) {
      setError(err instanceof Error ? err.message : "Failed to fetch accounts");
      console.error("Account fetch error:", err);
    } finally {
      setLoading(false);
    }
  }, []);

  // Добавление счета
  const addAccount = useCallback((account: AccountResponse) => {
    setAccounts(prev => [...prev, account]);
  }, []);

  // Обновление счета
  const updateAccount = useCallback((updatedAccount: AccountResponse) => {
    setAccounts(prev =>
      prev.map(account =>
        account.id === updatedAccount.id ? updatedAccount : account
      )
    );
  }, []);

  // Удаление счета
  const deleteAccount = useCallback((id: string) => {
    setAccounts(prev => prev.filter(account => account.id !== id));
    // Сбрасываем выбранный счет, если он был удален
    setSelectedAccount(prev => prev?.id === id ? null : prev);
  }, []);

  // Выбор счета
  const selectAccount = useCallback((account: AccountResponse | null) => {
    setSelectedAccount(account);
  }, []);

  // Сброс выбранного счета
  const clearSelectedAccount = useCallback(() => {
    setSelectedAccount(null);
  }, []);

  // Получение счета по ID
  const getAccountById = useCallback(
    (id: string) => accounts.find(account => account.id === id),
    [accounts]
  );

  // Автоматическая загрузка при монтировании
  useEffect(() => {
    fetchAccounts();
  }, [fetchAccounts]);

  // Мемоизация значения контекста
  const contextValue = useMemo(() => ({
    accounts,
    loading,
    error,
    addAccount,
    updateAccount,
    deleteAccount,
    selectAccount,
    clearSelectedAccount,
    fetchAccounts,
    getAccountById,
    selectedAccount
  }), [
    accounts,
    loading,
    error,
    addAccount,
    updateAccount,
    deleteAccount,
    selectAccount,
    clearSelectedAccount,
    fetchAccounts,
    getAccountById,
    selectedAccount
  ]);

  return (
    <AccountContext.Provider value={contextValue}>
      {children}
    </AccountContext.Provider>
  );
};