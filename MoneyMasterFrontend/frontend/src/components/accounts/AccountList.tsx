import React from "react";
import { AccountResponse, AccountType } from "@/types/account";
import AccountItem from "@/components/accounts/AccountItem"

interface Props {
  accounts: AccountResponse[];
  accountTypes: AccountType[];
  selectedAccount: AccountResponse | null;
  setSelectedAccount: (account: AccountResponse | null) => void;
}

const AccountList: React.FC<Props> = ({
  accounts,
  accountTypes,
  selectedAccount,
  setSelectedAccount,
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
        />
      ))}
    </ul>
  );
};

export default AccountList;
