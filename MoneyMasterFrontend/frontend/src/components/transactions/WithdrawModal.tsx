import React, { useState } from "react";
import Modal from "../Modal";
import {
  AccountResponse,
  CategoryResponse,
  CreateTransactionRequest,
} from "@/types";
import PlusIcon from "@/assets/icons/plus.svg?react";

interface WithdrawModalProps {
  isOpen: boolean;
  onClose: () => void;
  account: AccountResponse;
  categories: CategoryResponse[];
  onConfirm: (data: CreateTransactionRequest) => void;
}

const WithdrawModal: React.FC<WithdrawModalProps> = ({
  isOpen,
  onClose,
  account,
  categories,
  onConfirm,
}) => {
  const [amount, setAmount] = useState<number | string>("");
  const [selectedCategory, setSelectedCategory] =
    useState<CategoryResponse | null>(null);
  const [description, setDescription] = useState<string>("");

  const filteredCategories = categories.filter(
    (category) => category.categoryType === "Expenses"
  );

  const handleSubmit = () => {
    const numericAmount = Number(amount || 0);
    if (numericAmount > 0) {
      onConfirm({
        amount: numericAmount,
        categoryId: selectedCategory?.id || "",
        description,
        accountId: account.id,
      });

      setAmount("");
      setSelectedCategory(null);
      setDescription("");
      onClose();
    }
  };

  return (
    <Modal
      isOpen={isOpen}
      onClose={onClose}
      title={`Списать с ${account.name}`}
    >
      <div className="mb-4 space-y-4">
        <input
          type="number"
          value={amount}
          onChange={(e) => setAmount(e.target.value)}
          className="mt-2 p-2 w-full border border-gray-300 rounded-lg"
          placeholder="Сумма"
        />

        <div className="flex items-center space-x-2">
          <select
            value={selectedCategory?.id || ""}
            onChange={(e) => {
              const selectedCategory = filteredCategories.find(
                (category) => category.id === e.target.value
              );
              setSelectedCategory(selectedCategory || null);
            }}
            className="h-9 block w-full rounded-md bg-white px-3 py-1.5 text-base text-gray-900 outline-1 outline-gray-300 focus:outline-indigo-600"
          >
            <option value="">Выберите категорию</option>
            {filteredCategories.map((category) => (
              <option key={category.id} value={category.id}>
                {category.name}
              </option>
            ))}
          </select>
          <button className="bg-green-500 text-white px-3 py-1 rounded-md hover:bg-green-600 cursor-pointer">
            <PlusIcon className="w-7 h-7" />
          </button>
        </div>

        <input
          type="text"
          value={description}
          onChange={(e) => setDescription(e.target.value)}
          className="p-2 w-full border border-gray-300 rounded-lg"
          placeholder="Описание списания"
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
          className="bg-red-500 text-white px-4 py-2 rounded-lg hover:bg-red-600"
        >
          Списать
        </button>
      </div>
    </Modal>
  );
};

export default WithdrawModal;
