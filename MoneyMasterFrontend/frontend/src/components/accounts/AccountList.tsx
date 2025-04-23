import React from "react";
import AccountItem, { Account, AccountType } from "./AccountItem";

interface Props {
  accounts: Account[];
  accountTypes: AccountType[];
  selectedAccount: Account | null;
  setSelectedAccount: (account: Account | null) => void;
  handleDeleteAccount: (id: string) => void;
}

const AccountList: React.FC<Props> = ({
  accounts,
  accountTypes,
  selectedAccount,
  setSelectedAccount,
  handleDeleteAccount,
}) => {
  if (accounts.length === 0) {
    return <p className="text-gray-500">Счетов пока нет.</p>;
  }

  return (
    <ul className="space-y-4">
      {accounts.map((account) => (
        <AccountItem
          key={account.id}
          account={account}
          accountTypes={accountTypes}
          selectedAccount={selectedAccount}
          setSelectedAccount={setSelectedAccount}
          handleDeleteAccount={handleDeleteAccount}
        />
      ))}
    </ul>
  );
};

export default AccountList;
