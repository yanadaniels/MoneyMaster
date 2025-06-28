import React, { useState } from "react";
import Modal from "../Modal";
import {
  AccountResponse,
  CategoryResponse,
  CreateTransactionRequest,
} from "@/types";
import PlusIcon from "@/assets/icons/plus.svg?react";
import MinusIcon from "@/assets/icons/minus.svg?react";
import AddCategory from "../categories/AddCategory";

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
  const [addCategory, setAddCategory] = useState(false);
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

  const handleAddNewCategory = (newCategory : CategoryResponse | null) => {
    setAddCategory(false);
    setSelectedCategory(newCategory);
  }

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
          className="mt-1 block w-full px-3 py-2 border border-gray-300 rounded-lg focus:outline-none focus:ring-2 focus:ring-blue-500"
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
            className={`mt-1 block w-full px-2 py-2 border border-gray-300 rounded-lg focus:outline-none focus:ring-2 focus:ring-blue-500 ${
              addCategory
                ? "bg-gray-200 text-gray-500 opacity-50 cursor-not-allowed"
                : "bg-white text-gray-900"
            }`}
            disabled={addCategory}
          >
            <option value="">Выберите категорию</option>
            {filteredCategories.map((category) => (
              <option key={category.id} value={category.id}>
                {category.name}
              </option>
            ))}
          </select>
          <button className="bg-green-500 text-white px-3 py-1 rounded-md hover:bg-green-600 cursor-pointer">
            {addCategory ? (
              <MinusIcon
                className="w-7 h-7"
                onClick={() => setAddCategory(false)}
              />
            ) : (
              <PlusIcon
                className="w-7 h-7"
                onClick={() => setAddCategory(true)}
              />
            )}
          </button>
        </div>

        {addCategory && (
          <AddCategory categoryType="Expenses" onCategoryAdded={handleAddNewCategory} />
        )}

        <input
          type="text"
          value={description}
          onChange={(e) => setDescription(e.target.value)}
          className="mt-1 block w-full px-3 py-2 border border-gray-300 rounded-lg focus:outline-none focus:ring-2 focus:ring-blue-500"
          placeholder="Описание"
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
