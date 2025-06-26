import React from "react";
import { formatCurrency } from "@/utils/format";
import IconRenderer from "@/components/IconRenderer";
import { AccountResponse, AccountType } from "@/types";

interface Props {
  account: AccountResponse;
  accountTypes: AccountType[];
  selectedAccount: AccountResponse | null;
  setSelectedAccount: (account: AccountResponse | null) => void;
}

const AccountItem: React.FC<Props> = ({
  account,
  accountTypes,
  selectedAccount,
  setSelectedAccount,
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
    </li>
  );
};

export default AccountItem;
