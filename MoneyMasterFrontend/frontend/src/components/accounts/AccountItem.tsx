import React from "react";
import { formatCurrency } from "@/utils/format";
import IconRenderer from "@/components/IconRenderer";

// Типы
export interface Account {
  id: string;
  name: string;
  balance: number;
  currency: string;
  icon: string;
  userId: string;
  accountTypeId: string;
  createAt: string;
}

export interface AccountType {
  id: string;
  name: string;
  icon: string;
  isSystem: boolean;
  isDelete: boolean;
  createAt: string;
}

interface Props {
  account: Account;
  accountTypes: AccountType[];
  selectedAccount: Account | null;
  setSelectedAccount: (account: Account | null) => void;
  handleDeleteAccount: (id: string) => void;
}

const AccountItem: React.FC<Props> = ({
  account,
  accountTypes,
  selectedAccount,
  setSelectedAccount,
  handleDeleteAccount,
}) => {
  const isSelected = selectedAccount?.id === account.id;
  const accountTypeName =
    accountTypes.find((type) => type.id === account.accountTypeId)?.name ||
    "Счет без типа";

  return (
    <li
      className={`space-y-2 shadow-md rounded-lg p-4 hover:bg-blue-100 cursor-pointer ${
        isSelected ? "bg-blue-100" : "bg-white"
      }`}
      onClick={() => {
        if (selectedAccount?.id === account.id) {
          setSelectedAccount(null);
        } else {
          setSelectedAccount(account);
        }
      }}
    >
      <div className="flex  items-center justify-between pb-2">
        <span className="text-sm text-gray-600">{accountTypeName}</span>
        <IconRenderer
          iconName={account.icon}
          className="text-2xl text-blue-500"
        />
      </div>

      <div className="flex justify-between w-full">
        <span className="font-semibold">{account.name}</span>
        <span>{formatCurrency(account.balance)}</span>
      </div>

      {/* <button
        onClick={(e) => {
          e.stopPropagation();
          handleDeleteAccount(account.id);
        }}
        className="bg-red-500 text-white px-3 py-1 rounded hover:bg-red-600 transition"
      >
        Удалить
      </button> */}
    </li>
  );
};

export default AccountItem;
