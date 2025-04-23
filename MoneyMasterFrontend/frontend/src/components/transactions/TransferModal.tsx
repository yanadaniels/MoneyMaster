import React, { useState, useEffect } from "react";
import { useAccountContext } from "@/Context/AccountContext";
import { formatCurrency } from "@/utils/format";
import Modal from "../Modal";
import {
  AccountResponse,
  CategoryResponse,
  CreateTransactionTransferRequest,
  AccountType,
} from "@/types";

interface TransferModalProps {
  isOpen: boolean;
  onClose: () => void;
  account: AccountResponse;
  categories: CategoryResponse[];
  accountTypes: AccountType[];
  onConfirm: (data: CreateTransactionTransferRequest) => void;
}

const TransferModal: React.FC<TransferModalProps> = ({
  isOpen,
  onClose,
  account,
  categories,
  accountTypes,
  onConfirm,
}) => {
  const { accounts } = useAccountContext();

  const [fromAccountId, setFromAccountId] = useState<string>(account.id);
  const [fromCategoryId, setFromCategoryId] = useState<CategoryResponse | null>(
    null
  );
  const [toCategoryId, setToCategoryId] = useState<CategoryResponse | null>(
    null
  );
  const [toAccountId, setToAccountId] = useState<string | null>(null);
  const [amount, setAmount] = useState<number | string>("");
  const [description, setDescription] = useState<string>("");

  const filteredAccounts = accounts.filter((acnt) => acnt.id != account.id);

  useEffect(() => {
    setFromAccountId(account.id);
    const filteredRevenueCategories = categories.filter(
      (category) =>
        category.categoryType === "Revenue" && category.name === "Перевод"
    );
    const filteredExpensesCategories = categories.filter(
      (category) =>
        category.categoryType === "Expenses" && category.name === "Перевод"
    );
    setFromCategoryId(filteredExpensesCategories[0]);
    setToCategoryId(filteredRevenueCategories[0]);
  }, [account, categories]);

  const handleSubmit = () => {
    const numericAmount = Number(amount || 0);
    if (numericAmount > 0 && toAccountId) {
      onConfirm({
        fromAccountId,
        fromCategoryId: fromCategoryId?.id || "",
        toAccountId,
        toCategoryId: toCategoryId?.id || "",
        amount: numericAmount,
        description,
      });

      setAmount("");
      setFromAccountId("");
      setToAccountId("");
      setDescription("");
      onClose();
    }
  };

  return (
    <Modal
      isOpen={isOpen}
      onClose={onClose}
      title={`Пополнить ${account.name}`}
    >
      <div className="mb-4 space-y-4">
        <select
          value={toAccountId || ""}
          onChange={(e) => {
            const selectedToAccountId = filteredAccounts.find(
              (account) => account.id === e.target.value
            );
            setToAccountId(selectedToAccountId?.id || null);
          }}
          className="h-9 block w-full rounded-md bg-white px-3 py-1.5 text-base text-gray-900 outline-1 outline-gray-300 focus:outline-indigo-600"
        >
          <option value="" disabled hidden>
            Счет зачисления
          </option>
          {filteredAccounts.map((account) => (
            <option key={account.id} value={account.id}>
              {account.name} (
              {
                accountTypes.find((type) => type.id === account.accountTypeId)
                  ?.name
              }
              ) - {formatCurrency(account.balance)}
            </option>
          ))}
        </select>

        <input
          type="number"
          inputMode="numeric"
          pattern="[0-9]"
          value={amount}
          onChange={(e) => setAmount(e.target.value)}
          className="mt-2 p-2 w-full border border-gray-300 rounded-lg appearance-none [&::-webkit-outer-spin-button]:appearance-none [&::-webkit-inner-spin-button]:appearance-none"
          placeholder="Сумма"
        />

        <input
          type="text"
          value={description}
          onChange={(e) => setDescription(e.target.value)}
          className="p-2 w-full border border-gray-300 rounded-lg"
          placeholder="Описание пополнения"
        />
      </div>

      <div className="flex justify-end gap-2 mt-8">
        <button
          onClick={onClose}
          className="bg-gray-500 text-white px-4 py-2 rounded-lg hover:bg-gray-600"
        >
          Отменить
        </button>
        <button
          onClick={handleSubmit}
          className="bg-green-500 text-white px-4 py-2 rounded-lg hover:bg-green-600"
        >
          Пополнить
        </button>
      </div>
    </Modal>
  );
};

export default TransferModal;
