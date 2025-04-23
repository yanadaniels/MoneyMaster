import React, { useState } from "react";
import Modal from "@components/Modal"; // Импортируем модальный компонент
import { AccountCreateInfo, AccountType } from "@/types";
import { accountService } from "@/services/accountService"; // Импортируем сервис для работы с аккаунтами
import { useAuth } from "../../Context/AuthContext";
import IconPicker from "../IconPicker";

interface AccountAddModalProps {
  isOpen: boolean;
  onClose: () => void;
  accountCreateInfo: AccountCreateInfo | null;
  onCreateAccount: (newAccount: any) => void; // Вызываем колбек для добавления аккаунта в родительский компонент
}

const AccountAddModal: React.FC<AccountAddModalProps> = ({
  isOpen,
  onClose,
  accountCreateInfo,
  onCreateAccount,
}) => {
  const { state } = useAuth();
  const [newAccountName, setNewAccountName] = useState("");
  const [selectAccountType, setSelectAccountType] =
    useState<AccountType | null>(null);
  const [selectCurrency, setSelectCurrency] = useState("");
  const [newAccountBalance, setNewAccountBalance] = useState("");
  const [selectedIcon, setSelectedIcon] = useState("faWallet");
  const [isLoading, setIsLoading] = useState(false);

  const handleCreateAccount = async () => {
    try {
      setIsLoading(true); // Устанавливаем загрузку перед запросом

      // Выполняем запрос на создание счета
      const newAccount = await accountService.createAccount(
        state.user?.id,
        newAccountName,
        parseFloat(newAccountBalance),
        selectAccountType?.id,
        selectCurrency,
        selectedIcon
      );

      // Обновляем родительский компонент с новым счетом
      onCreateAccount(newAccount);

      // Сбрасываем состояние
      setNewAccountName("");
      setNewAccountBalance("");
      setSelectAccountType(null);
      setSelectCurrency("");
      setSelectedIcon("faWallet");
      onClose();
    } catch (error) {
      console.error("Ошибка при создании счета:", error);
    } finally {
      setIsLoading(false); // Снимаем состояние загрузки
    }
  };

  return (
    <Modal isOpen={isOpen} onClose={onClose} title="Создать новый счет">
      <div className="space-y-4">
        <input
          type="text"
          placeholder="Название счета"
          value={newAccountName}
          onChange={(e) => setNewAccountName(e.target.value)}
          className="block w-full rounded-md bg-white px-3 py-1.5 text-base outline-1 outline-gray-300 placeholder:text-gray-400 focus:outline-indigo-600"
        />
        <IconPicker selectedIcon={selectedIcon} onSelect={setSelectedIcon} />
        <select
          value={selectAccountType?.id || ""}
          onChange={(e) => {
            const selectedType = accountCreateInfo?.accountTypes.find(
              (type) => type.id === e.target.value
            );
            setSelectAccountType(selectedType || null);
          }}
          className={`h-9 block w-full rounded-md bg-white px-2 py-1.5 text-base outline-1 outline-gray-300 focus:outline-indigo-600
            ${selectAccountType === null ? "text-gray-400" : "text-black"} 
            focus:text-black bg-white focus:outline-indigo-600`}
        >
          <option value="" disabled hidden>
            Выберите тип счета
          </option>
          {accountCreateInfo?.accountTypes.map((type) => (
            <option key={type.id} value={type.id}>
              {type.name}
            </option>
          ))}
        </select>

        <select
          value={selectCurrency}
          onChange={(e) => setSelectCurrency(e.target.value)}
          className={`h-9 block w-full rounded-md bg-white px-2 py-1.5 text-base outline-1 outline-gray-300 text-gray-900" focus:outline-indigo-600     
            ${selectCurrency === "" ? "text-gray-400" : "text-black"} 
            focus:text-black bg-white focus:outline-indigo-600`}
        >
          <option value="" disabled hidden className="text-gray-400">
            Выберите валюту
          </option>
          {accountCreateInfo?.currentCode.map((currency, index) => (
            <option key={index} value={currency}>
              {currency}
            </option>
          ))}
        </select>

        <input
          type="text"
          placeholder="Баланс"
          value={newAccountBalance}
          onChange={(e) => {
            const value = e.target.value;
            if (/^\d*\.?\d*$/.test(value)) {
              setNewAccountBalance(value);
            }
          }}
          className="block w-full rounded-md bg-white px-3 py-1.5 text-base text-gray-900 outline-1 outline-gray-300 placeholder:text-gray-400 focus:outline-indigo-600"
        />
      </div>

      <div className="flex justify-end gap-2 mt-10">
        <button
          onClick={onClose}
          className="bg-gray-400 text-white px-4 py-2 rounded"
        >
          Отмена
        </button>
        <button
          onClick={handleCreateAccount}
          className="bg-green-500 text-white px-4 py-2 rounded"
          disabled={isLoading} // Отключаем кнопку при загрузке
        >
          {isLoading ? "Создание..." : "Создать"}
        </button>
      </div>
    </Modal>
  );
};

export default AccountAddModal;
