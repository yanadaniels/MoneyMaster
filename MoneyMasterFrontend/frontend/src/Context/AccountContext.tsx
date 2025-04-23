// AccountContext.tsx
import React, { createContext, useContext, useState, ReactNode } from "react";
import { AccountResponse } from "@/types";

interface AccountContextType {
  accounts: AccountResponse[];
  addAccount: (account: AccountResponse) => void;
  setAccounts: React.Dispatch<React.SetStateAction<AccountResponse[]>>;
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
}

export const AccountProvider: React.FC<AccountProviderProps> = ({
  children,
}) => {
  const [accounts, setAccounts] = useState<AccountResponse[]>([]);

  const addAccount = (account: AccountResponse) => {
    setAccounts((prev) => [...prev, account]);
  };

  return (
    <AccountContext.Provider value={{ accounts, addAccount, setAccounts }}>
      {children}
    </AccountContext.Provider>
  );
};
