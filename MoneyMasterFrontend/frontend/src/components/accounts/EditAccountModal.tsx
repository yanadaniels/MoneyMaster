import { useState, useEffect } from "react";
import IconPicker from "@/components/IconPicker";
import { UpdatingAccountRequest, AccountResponse, AccountType } from "@/types";
import Modal from "@/components/Modal";

interface EditAccountModalProps {
  isOpen: boolean;
  onClose: () => void;
  account: AccountResponse;
  accountTypes: AccountType[];
  onUpdateAccount: (accountId: UpdatingAccountRequest) => void;
  onDeleteAccount: (id: string) => void;
}

const EditAccountModal: React.FC<EditAccountModalProps> = ({
  isOpen,
  onClose,
  account,
  accountTypes,
  onUpdateAccount,
  onDeleteAccount,
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
    onUpdateAccount({
      id: account.id,
      name,
      balance,
      currency: account.currency,
      icon,
      accountTypeId: accountType,
      userId: account.userId
    });
    onClose();
  };

  const handleDelete = (accountId: string) => {
    onDeleteAccount(accountId);
    onClose();
  }

  return (
    <Modal isOpen={isOpen} onClose={onClose} title="Редактировать счёт">
      <div className="space-y-4">
        <div>
          <label className="block text-sm font-medium">Название</label>
          <input
            type="text"
            className="mt-1 block w-full px-3 py-2 border border-gray-300 rounded-lg focus:outline-none focus:ring-2 focus:ring-blue-500"
            value={name}
            onChange={(e) => setName(e.target.value)}
          />
        </div>

        <IconPicker selectedIcon={icon} onSelect={setIcon} />

        <div>
          <label className="block text-sm font-medium">Баланс</label>
          <input
            type="number"
            className="mt-1 block w-full px-3 py-2 border border-gray-300 rounded-lg focus:outline-none focus:ring-2 focus:ring-blue-500"
            value={balance}
            onChange={(e) => setBalance(Number(e.target.value))}
          />
        </div>

        <div>
          <label className="block text-sm font-medium">Тип счета</label>
          <select
            className="mt-1 block w-full px-2 py-2 border border-gray-300 rounded-lg focus:outline-none focus:ring-2 focus:ring-blue-500"
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

        <div className="flex justify-end gap-2 mt-6">
          <button
            onClick={(e) => {
              e.stopPropagation();
              handleDelete(account.id);
            }}
            className="bg-red-500 text-white px-3 py-1 rounded hover:bg-red-600 transition mr-auto"
          >
            Удалить
          </button>
          <button
            className="bg-gray-500 text-white px-4 py-2 rounded-lg hover:bg-gray-600"
            onClick={onClose}
          >
            Отмена
          </button>
          <button
            className="px-4 py-2 bg-blue-600 text-white rounded-lg hover:bg-blue-800"
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
