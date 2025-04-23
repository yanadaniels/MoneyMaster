import { useState, useEffect } from "react";
import IconPicker from "@/components/IconPicker";
import { AccountResponse, AccountType } from "@/types";
import Modal from "@/components/Modal";

interface EditAccountModalProps {
  isOpen: boolean;
  onClose: () => void;
  account: AccountResponse;
  accountTypes: AccountType[];
  onSave: (data: AccountResponse) => void;
  handleDeleteAccount: (id: string) => void;
}

const EditAccountModal: React.FC<EditAccountModalProps> = ({
  isOpen,
  onClose,
  account,
  accountTypes,
  onSave,
  handleDeleteAccount,
}) => {
  const [name, setName] = useState(account.name);
  const [balance, setBalance] = useState(account.balance);
  const [icon, setIcon] = useState(account.icon || "");
  const [accountType, setAccountType] = useState(account.accountTypeId || "");

  useEffect(() => {
    if (isOpen) {
      setName(account.name);
      setBalance(account.balance);
      setIcon(account.icon || "");
      setAccountType(account.accountTypeId);
    }
  }, [account, isOpen]);

  const handleSubmit = () => {
    onSave({
      ...account,
      name,
      balance,
      icon,
      accountTypeId: accountType,
    });
    onClose();
  };

  return (
    <Modal isOpen={isOpen} onClose={onClose} title="Редактировать счёт">
      <div className="space-y-4">
        <div>
          <label className="block text-sm font-medium">Название</label>
          <input
            type="text"
            className="w-full border border-gray-300 rounded-lg p-2 mt-1"
            value={name}
            onChange={(e) => setName(e.target.value)}
          />
        </div>

        <IconPicker selectedIcon={icon} onSelect={setIcon} />

        <div>
          <label className="block text-sm font-medium">Баланс</label>
          <input
            type="number"
            className="w-full border border-gray-300 rounded-lg p-2 mt-1"
            value={balance}
            onChange={(e) => setBalance(Number(e.target.value))}
          />
        </div>

        <div>
          <label className="block text-sm font-medium">Тип счета</label>
          <select
            className="w-full border border-gray-300 rounded-lg p-2 mt-1"
            value={accountType}
            onChange={(e) => setAccountType(e.target.value)}
          >
            <option value="">Выберите тип счета</option>
            {accountTypes.map((type) => (
              <option key={type.id} value={type.id}>
                {type.name}
              </option>
            ))}
          </select>
        </div>

        <button
          onClick={(e) => {
            e.stopPropagation();
            handleDeleteAccount(account.id);
          }}
          className="bg-red-500 text-white px-3 py-1 rounded hover:bg-red-600 transition"
        >
          Удалить
        </button>

        <div className="flex justify-end gap-2 mt-6">
          <button
            className="px-4 py-2 bg-gray-200 rounded-lg"
            onClick={onClose}
          >
            Отмена
          </button>
          <button
            className="px-4 py-2 bg-blue-600 text-white rounded-lg"
            onClick={handleSubmit}
          >
            Сохранить
          </button>
        </div>
      </div>
    </Modal>
  );
};

export default EditAccountModal;
