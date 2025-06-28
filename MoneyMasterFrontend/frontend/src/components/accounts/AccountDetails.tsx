import { useState } from "react";
import { useTransactionContext } from "@/Context/TransactionContext";
import { useCategoryContext } from "@/Context/CategoryContext";
import DepositModal from "@/components/transactions/DepositModal";
import WithdrawModal from "@/components/transactions/WithdrawModal";
import TransferModal from "@/components/transactions/TransferModal";
import EditAccountModal from "@/components/accounts/EditAccountModal";
import {
  CreateTransactionRequest,
  CreateTransactionTransferRequest,
  AccountResponse,
  AccountType,
  UpdatingAccountRequest
} from "@/types";
import { transactionService } from "@/services/transactionService";
import TransactionHistory from "../transactions/TransactionHistory";
import { formatCurrency } from "@/utils/format";

import SettingsIcon from "@/assets/icons/settings.svg?react";
import PlusIcon from "@/assets/icons/plus.svg?react";
import MinusIcon from "@/assets/icons/minus.svg?react";
import TransferIcon from "@/assets/icons/transfer.svg?react";

interface AccountDetailsProps {
  selectedAccount: AccountResponse;
  accountTypes: AccountType[];
  onDeleteAccount: (accountId: string) => void;
  onUpdateAccount: (updatedAccount: UpdatingAccountRequest) => void;
}

const AccountDetails: React.FC<AccountDetailsProps> = ({
  selectedAccount,
  accountTypes,
  onDeleteAccount,
  onUpdateAccount
}) => {
  const [isDepositModalOpen, setIsDepositModalOpen] = useState(false);
  const [isWithdrawModalOpen, setIsWithdrawModalOpen] = useState(false);
  const [isTransferModalOpen, setIsTransferModalOpen] = useState(false);
  const [isAccountSettingsModalOpen, setIsAccountSettingsModalOpen] = useState(false);
  const { categories, loading, error } = useCategoryContext();
  const { addTransaction } = useTransactionContext();

  const handleCreateTransaction = async (data: CreateTransactionRequest) => {
    try {
      const id = await transactionService.createTransaction({
        amount: data.amount,
        categoryId: data.categoryId,
        description: data.description,
        accountId: data.accountId,
      });

      if (id) {
        const newTransaction = await transactionService.getTransactionById(id);
        if (newTransaction) {
          addTransaction(newTransaction);
        }
      }
    } catch (error) {
      console.error("Ошибка при создании транзакции:", error);
    } finally {
      //setIsLoading(false); // Снимаем состояние загрузки
    }
  };

  const handleCreateTransactionTransfer = async (
    data: CreateTransactionTransferRequest
  ) => {
    try {
      const id = await transactionService.createTransactionTransfer({
        amount: data.amount,
        fromAccountId: data.fromAccountId,
        fromCategoryId: data.fromCategoryId,
        toAccountId: data.toAccountId,
        toCategoryId: data.toCategoryId,
        description: data.description,
      });
      if (id) {
        const newTransaction = await transactionService.getTransactionById(id);
        if (newTransaction) {
          addTransaction(newTransaction);
        }
      }
    } catch (error) {
      console.error(
        "Ошибка при создании транзакции перевода между счетами:",
        error
      );
    } finally {
      //setIsLoading(false)
    }
  };

  const accountTypeName =
    accountTypes.find((type) => type.id === selectedAccount.accountTypeId)?.name ||
    "Счет без типа";

  if (loading) return <div>Загрузка...</div>;
  if (error) return <div>Ошибка: {error}</div>;

  return (
    <div className="grid grid-rows-[auto_1fr_auto]">
      <div className="rounded-md bg-white shadow-md p-4">
        <div className="flex items-center justify-between ">
          <div>
            <h2 className="text-xl font-semibold">{selectedAccount.name}</h2>
            <p>{accountTypeName}</p>
          </div>
          <p className="flex items-center text-xl text-gray-600">
            <strong>{formatCurrency(selectedAccount.balance)}</strong>
            <SettingsIcon
              onClick={() => setIsAccountSettingsModalOpen(true)}
              className="w-8 h-8 ml-5 transaction-transform duration-300 hover:rotate-45"
            />
          </p>
        </div>

        <div className="mt-2 space-x-2">
          <button
            onClick={() => setIsDepositModalOpen(true)}
            className="bg-green-500 text-white px-4 py-2 rounded-md hover:bg-green-600 cursor-pointer"
            title="Поступление"
          >
            <PlusIcon className="w-8 h-8" />
          </button>
          <button
            onClick={() => setIsWithdrawModalOpen(true)}
            className="bg-red-500 text-white px-4 py-2 rounded-md hover:bg-red-600 cursor-pointer"
            title="Расход"
          >
            <MinusIcon className="w-8 h-8" />
          </button>
          <button
            onClick={() => setIsTransferModalOpen(true)}
            className="bg-yellow-500 text-white px-4 py-2 rounded-md hover:bg-yellow-600 cursor-pointer"
            title="Перевод"
          >
            <TransferIcon className="w-8 h-8" />
          </button>
        </div>
      </div>

      <TransactionHistory selectedAccount={selectedAccount}/>

      <DepositModal
        isOpen={isDepositModalOpen}
        onClose={() => setIsDepositModalOpen(false)}
        account={selectedAccount}
        categories={categories}
        onConfirm={handleCreateTransaction}
      />

      <WithdrawModal
        isOpen={isWithdrawModalOpen}
        onClose={() => setIsWithdrawModalOpen(false)}
        account={selectedAccount}
        categories={categories}
        onConfirm={handleCreateTransaction}
      />

      <TransferModal
        isOpen={isTransferModalOpen}
        onClose={() => setIsTransferModalOpen(false)}
        account={selectedAccount}
        categories={categories}
        accountTypes={accountTypes}
        onConfirm={handleCreateTransactionTransfer}
      />

      <EditAccountModal
        isOpen={isAccountSettingsModalOpen}
        onClose={() => setIsAccountSettingsModalOpen(false)}
        account={selectedAccount}
        accountTypes={accountTypes}
        onUpdateAccount={onUpdateAccount}
        onDeleteAccount={onDeleteAccount}
      />
    </div>
  );
};

export default AccountDetails;
