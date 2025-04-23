import { useEffect, useState } from "react";
import DepositModal from "@/components/transactions/DepositModal";
import WithdrawModal from "@/components/transactions/WithdrawModal";
import TransferModal from "@/components/transactions/TransferModal";
import EditAccountModal from "@/components/accounts/EditAccountModal";
import {
  CategoryResponse,
  CreateTransactionRequest,
  CreateTransactionTransferRequest,
  TransactionResponse,
  AccountResponse,
  AccountType,
} from "@/types";
import { transactionService } from "@/services/transactionService";
import { categoryService } from "@/services/categoryService";
import TransactionHistory from "../transactions/TransactionHistory";
import { accountService } from "@/services/accountService";
import { formatCurrency } from "@/utils/format";
import { useAccountContext } from "@/Context/AccountContext";

import SettingsIcon from "@/assets/icons/settings.svg?react";
import PlusIcon from "@/assets/icons/plus.svg?react";
import MinusIcon from "@/assets/icons/minus.svg?react";
import TransferIcon from "@/assets/icons/transfer.svg?react";

interface AccountDetailsProps {
  account: AccountResponse;
  accountTypes: AccountType[];
  onAccountUpdate: (updated: AccountResponse) => void;
}

const AccountDetails: React.FC<AccountDetailsProps> = ({
  account,
  accountTypes,
  onAccountUpdate,
}) => {
  const { setAccounts } = useAccountContext();
  const [isDepositModalOpen, setIsDepositModalOpen] = useState(false);
  const [isWithdrawModalOpen, setIsWithdrawModalOpen] = useState(false);
  const [isTransferModalOpen, setIsTransferModalOpen] = useState(false);
  const [isAccountSettingsModalOpen, setIsAccountSettingsModalOpen] =
    useState(false);
  const [transactions, setTransactions] = useState<TransactionResponse[]>([]);
  const [categories, setCategories] = useState<CategoryResponse[]>([]);

  useEffect(() => {
    const fetchCategories = async () => {
      try {
        const data = await categoryService.getCategories();
        console.log(data);
        setCategories(data);
      } catch (error) {
        console.error("Ошибка при загрузке категорий:", error);
      } finally {
        //setLoading(false);
      }
    };

    const fetchTransactions = async () => {
      try {
        const transactions = await transactionService.getTransactionByAccountId(
          account.id
        );
        console.log(transactions);

        if (transactions) {
          setTransactions(transactions);
        }
      } catch (error) {
        console.error("Ошибка при загрузке транзакций:", error);
      } finally {
        //setLoading(false);
      }
    };

    fetchCategories();
    fetchTransactions();
  }, [account]);

  const handleCreateTransaction = async (data: CreateTransactionRequest) => {
    try {
      //setIsLoading(true); // Устанавливаем загрузку перед запросом
      console.log(data);
      // Выполняем запрос на создание транзакции
      const id = await transactionService.createTransaction({
        amount: data.amount,
        categoryId: data.categoryId,
        description: data.description,
        accountId: data.accountId,
      });

      if (id) {
        const newTransaction = await transactionService.getTransactionById(id);

        if (newTransaction) {
          setTransactions((prev) => [...prev, newTransaction]);

          const updatedAccount = await accountService.getAccountById(
            account.id
          );

          if (updatedAccount) {
            onAccountUpdate(updatedAccount);
          }
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
    console.log(data);
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
          setTransactions((prev) => [...prev, newTransaction]);
          const updatedAccounts = await accountService.getAccounts();
          console.log("updatedAccouts", updatedAccounts);
          if (updatedAccounts) {
            setAccounts(updatedAccounts.data);
          }
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
    accountTypes.find((type) => type.id === account.accountTypeId)?.name ||
    "Счет без типа";

  return (
    <div className="h-screen">
      <div className="rounded-md bg-white shadow-md p-4">
        <div className="flex items-center justify-between ">
          <div>
            <h2 className="text-xl font-semibold">{account.name}</h2>
            <p>{accountTypeName}</p>
          </div>
          <p className="flex items-center text-xl text-gray-600">
            <strong>{formatCurrency(account.balance)}</strong>
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
          >
            <PlusIcon className="w-8 h-8" />
          </button>
          <button
            onClick={() => setIsWithdrawModalOpen(true)}
            className="bg-red-500 text-white px-4 py-2 rounded-md hover:bg-red-600 cursor-pointer"
          >
            <MinusIcon className="w-8 h-8" />
          </button>
          <button
            onClick={() => setIsTransferModalOpen(true)}
            className="bg-yellow-500 text-white px-4 py-2 rounded-md hover:bg-yellow-600 cursor-pointer"
          >
            <TransferIcon className="w-8 h-8" />
          </button>
        </div>
      </div>

      <TransactionHistory transactions={transactions} categories={categories} />

      <DepositModal
        isOpen={isDepositModalOpen}
        onClose={() => setIsDepositModalOpen(false)}
        account={account}
        categories={categories}
        onConfirm={handleCreateTransaction}
      />

      <WithdrawModal
        isOpen={isWithdrawModalOpen}
        onClose={() => setIsWithdrawModalOpen(false)}
        account={account}
        categories={categories}
        onConfirm={handleCreateTransaction}
      />

      <TransferModal
        isOpen={isTransferModalOpen}
        onClose={() => setIsTransferModalOpen(false)}
        account={account}
        categories={categories}
        accountTypes={accountTypes}
        onConfirm={handleCreateTransactionTransfer}
      />

      <EditAccountModal
        isOpen={isAccountSettingsModalOpen}
        onClose={() => setIsAccountSettingsModalOpen(false)}
        account={account}
        accountTypes={accountTypes}
        onSave={(updated) => {
          // логика обновления
          console.log("Обновлённый счёт:", updated);
        }}
        handleDeleteAccount={() => console.log("Удаление счета")}
      />
    </div>
  );
};

export default AccountDetails;
