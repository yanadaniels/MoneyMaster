import React, { useState, useEffect } from "react";
import Modal from "../Modal";
import {
  AccountResponse,
  CategoryResponse,
  TransactionResponse,
  UpdateTransactionRequest,
} from "@/types";
import PlusIcon from "@/assets/icons/plus.svg?react";
import MinusIcon from "@/assets/icons/minus.svg?react";
import AddCategory from "../categories/AddCategory";

interface UpdateModalProps {
  isOpen: boolean;
  onClose: () => void;
  selectedAccont: AccountResponse;
  transaction: TransactionResponse | null;
  categories: CategoryResponse[];
  onConfirm: (transactionId: string, data: UpdateTransactionRequest) => void;
  onDelete: (transactionId: string) => void;
}

const UpdateModal: React.FC<UpdateModalProps> = ({
  isOpen,
  onClose,
  selectedAccont,
  transaction,
  categories,
  onConfirm,
  onDelete,
}) => {
  const [amount, setAmount] = useState<number | string>(transaction ? transaction.amount.toString() : "");
  const [selectedCategory, setSelectedCategory] = useState<CategoryResponse | undefined>(transaction ? categories.find((c) => c.id === transaction.categoryId) : undefined);
  const [description, setDescription] = useState<string>(transaction ? transaction.description : "");
  const [addCategory, setAddCategory] = useState(false);

  const categoryMap = new Map(categories.map(category => [category.id, category]));
  const targetCategoryType = transaction && transaction.categoryId ? categoryMap.get(transaction.categoryId)?.categoryType : null;
  const filteredCategories = targetCategoryType 
    ? categories.filter(category => category.categoryType === targetCategoryType)
    : [];


    useEffect(() => {
    if (transaction) {
      setAmount(transaction.amount.toString());
      setSelectedCategory(categories.find(c => c.id === transaction.categoryId));
      setDescription(transaction.description);
    } else {
      setAmount("");
      setSelectedCategory(undefined);
      setDescription("");
    }
  }, [transaction, categories, isOpen]);

  const handleSubmit = () => {
    const numericAmount = Number(amount || 0);
    if (numericAmount > 0 && transaction) {
      onConfirm(transaction?.id, {
        amount: numericAmount,
        categoryId: selectedCategory?.id || "",
        description,
        accountId: selectedAccont.id,
      });

      setAmount("");
      setSelectedCategory(undefined);
      setDescription("");
      onClose();
    }
  };

  const handleDelete = () => {
    if (transaction) {
      onDelete(transaction?.id);
      onClose();
    }
  }

  const handleAddNewCategory = (newCategory : CategoryResponse | null) => {
    setAddCategory(false);
    if (newCategory) setSelectedCategory(newCategory);
  }

  return (
    <Modal
      isOpen={isOpen}
      onClose={onClose}
      title={`Редактирование транзакции`}
    >
      <div className="mb-4 space-y-4">
        <input
          type="number"
          inputMode="numeric"
          pattern="[0-9]"
          value={amount}
          onChange={(e) => setAmount(e.target.value)}
          className="mt-1 block w-full px-3 py-2 border border-gray-300 rounded-lg focus:outline-none focus:ring-2 focus:ring-blue-500"
          placeholder="Сумма"
        />

        <div className="flex items-center space-x-2">
          <select
            value={selectedCategory?.id || transaction?.categoryId}
            onChange={(e) => {
              const selectedCategory = filteredCategories.find(
                (category) => category.id === e.target.value
              );
              setSelectedCategory(selectedCategory || undefined);
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
          onClick={handleDelete}
          className="bg-red-500 text-white px-4 py-2 rounded-lg hover:bg-red-600 mr-auto"
        >
          Удалить
        </button>
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
          Ок
        </button>
      </div>
    </Modal>
  );
};

export default UpdateModal;
