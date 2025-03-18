import { useEffect, useState, useRef } from "react";
import { useAuth } from "../Context/AuthContext";
import authService from "../services/authService";
import { accountService } from "../services/accountService";
import { formatCurrency } from "../utils/format";
import AccountDetails from "./AccountDetails";

interface Account {
  id: string;
  name: string;
  balance: number;
  currency: string;
  icon: string;
  userId: string;
  accountTypeId: string;
  createAt: string;
}

type AccountType = {
  id: string;
  name: string;
  icon: string;
  isSystem: boolean;
  isDelete: boolean;
  createAt: string;
};

type CreateAccountInfo = {
  accountTypes: AccountType[];
  currentCode: string[];
};

const Accounts: React.FC = () => {
  const { state } = useAuth();
  const [accounts, setAccounts] = useState<Account[]>([]);
  const [loading, setLoading] = useState<boolean>(true);
  const [isModalOpen, setIsModalOpen] = useState<boolean>(false);
  const [newAccountName, setNewAccountName] = useState<string>("");
  const [newAccountBalance, setNewAccountBalance] = useState<string>("");
  const [selectAccountType, setSelectAccountType] = useState<{
    id: string;
    name: string;
  } | null>(null);
  const [accountCreateInfo, setAccountCreateInfo] =
    useState<CreateAccountInfo>();
  const [selectCurency, setSelectCurency] = useState<string>("");
  const isFetched = useRef(false);
  const [selectedAccount, setSelectedAccount] = useState<Account | null>(null);

  useEffect(() => {
    const fetchAccounts = async () => {
      try {
        if (!state.user || isFetched.current) return; // Проверяем, что пользователь загружен и не было запроса
        isFetched.current = true; // Устанавливаем флаг, что запрос уже был выполнен

        const accessToken = authService.getToken();
        if (!accessToken) return;

        const data = await accountService.getAccounts(accessToken);
        console.log(data);
        setAccounts(data.data);
      } catch (error) {
        console.error("Ошибка при запуске счетов:", error);
      } finally {
        setLoading(false);
      }
    };

    fetchAccounts();
  }, [state.user]);

  const handleModalOpen = () => {
    setIsModalOpen(true);
    handleGetCreateInfo();
  };

  const handleGetCreateInfo = async () => {
    try {
      const accessToken = authService.getToken();
      if (!accessToken) return;

      const createInfo = await accountService.getCreateInfo(accessToken);
      setAccountCreateInfo(createInfo);
    } catch (error) {
      console.error("Ошибка при получении информации для создания счета");
    }
  };

  const handleCreateAccount = async () => {
    try {
      const accessToken = authService.getToken();
      if (!accessToken) return;

      const newAccount = await accountService.createAccount(
        accessToken,
        state.user?.id,
        newAccountName,
        parseFloat(newAccountBalance),
        selectAccountType?.id,
        selectCurency
      );

      setAccounts([...accounts, newAccount]);
      setNewAccountName("");
      setNewAccountBalance("");
      setIsModalOpen(false);
    } catch (error) {
      console.error("Ошибка при создании счета:", error);
    }
  };

  const handleDeleteAccount = async (accountId: string) => {
    try {
      const accesToken = authService.getToken();
      if (!accesToken) return;

      await accountService.deleteAccount(accesToken, accountId);
      setAccounts(accounts.filter((account) => account.id !== accountId));
      setSelectedAccount(null);
    } catch (error) {
      console.error("Ошибка при удалении счета:", error);
    }
  };

  if (loading) return <p>Загрузка...</p>;

  return (
    <div className="p-4 flex w-full">
      <div className="w-1/6">
        <h2 className="text-xl font-bold mb-4">Мои счета</h2>

        {/* Кнопка для открытия модального окна */}
        <button
          onClick={() => handleModalOpen()}
          className="bg-blue-500 text-white px-4 py-2 rounded mb-4"
        >
          Добавить новый счет
        </button>

        {/* Popup (модальное окно) */}
        {isModalOpen && (
          <div className="fixed inset-0 items-center flex justify-center bg-black/50">
            <div className="bg-white p-6 rounded shadow-lg w-96 space-y-4">
              <h3 className="text-lg font-semibold mb-4">Создать новый счет</h3>
              <div className="space-y-4">
                <input
                  type="text"
                  placeholder="Название счета"
                  value={newAccountName}
                  onChange={(e) => setNewAccountName(e.target.value)}
                  className="block w-full rounded-md bg-white px-3 py-1.5 text-base text-gray-900 outline-1 
                     -outline-offset-1 outline-gray-300 placeholder:text-gray-400 focus:outline-2
                     focus:-outline-offset-2 focus:outline-indigo-600 sm:text-sm/6"
                />

                <select
                  value={selectAccountType?.id || ""}
                  onChange={(e) => {
                    const selectedType = accountCreateInfo?.accountTypes.find(
                      (type) => type.id === e.target.value
                    );
                    setSelectAccountType(selectedType || null);
                  }}
                  className="h-9 block w-full rounded-md bg-white px-3 py-1.5 text-base text-gray-900 outline-1 
                     -outline-offset-1 outline-gray-300 placeholder:text-gray-400 focus:outline-2
                     focus:-outline-offset-2 focus:outline-indigo-600 sm:text-sm/6"
                >
                  <option value="">Выберите тип счета</option>
                  {accountCreateInfo?.accountTypes.map((type) => (
                    <option key={type.id} value={type.id}>
                      {type.name}
                    </option>
                  ))}
                </select>

                <select
                  value={selectCurency}
                  onChange={(e) => setSelectCurency(e.target.value)}
                  className="h-9 block w-full rounded-md bg-white px-3 py-1.5 text-base text-gray-900 outline-1 
                     -outline-offset-1 outline-gray-300 placeholder:text-gray-400 focus:outline-2
                     focus:-outline-offset-2 focus:outline-indigo-600 sm:text-sm/6"
                >
                  <option value="">Выберите валюту</option>
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
                  className="block w-full rounded-md bg-white px-3 py-1.5 text-base text-gray-900 outline-1 
                     -outline-offset-1 outline-gray-300 placeholder:text-gray-400 focus:outline-2
                     focus:-outline-offset-2 focus:outline-indigo-600 sm:text-sm/6"
                />
              </div>

              <div className="flex justify-end gap-2">
                <button
                  onClick={() => setIsModalOpen(false)}
                  className="bg-gray-400 text-white px-4 py-2 rounded"
                >
                  Отмена
                </button>
                <button
                  onClick={handleCreateAccount}
                  className="bg-green-500 text-white px-4 py-2 rounded"
                >
                  Создать
                </button>
              </div>
            </div>
          </div>
        )}

        {/* Список счетов */}
        {accounts.length === 0 ? (
          <p className="text-gray-500">Счетов пока нет.</p>
        ) : (
          <ul className="space-y-4">
            {accounts.map((account) => (
              <li
                key={account.id}
                className={`
                  flex flex-col justify-between items-left space-y-2 shadow-md rounded-lg p-4 hover:bg-blue-100 cursor-pointer 
                  ${
                    selectedAccount?.id === account.id
                      ? "bg-blue-100 border-blue-400"
                      : "bg-white"
                  }`}
                onClick={() =>
                  setSelectedAccount((prev) =>
                    prev?.id === account.id ? null : account
                  )
                }
              >
                <div className="max-w-full">
                  <span>
                    {accountCreateInfo?.accountTypes.find(
                      (type) => type.id === account.accountTypeId
                    )?.name || "Счет без типа"}
                  </span>
                </div>

                <div className="flex justify-between">
                  <span className="font-semibold">{account.name}</span>
                  <span>{formatCurrency(account.balance)}</span>
                </div>
                <button
                  onClick={() => handleDeleteAccount(account.id)}
                  className="bg-red-500 text-white px-3 py-1 rounded"
                >
                  Удалить
                </button>
              </li>
            ))}
          </ul>
        )}
      </div>

      <div className="pl-6 w-full">
        {selectedAccount ? (
          <AccountDetails account={selectedAccount} />
        ) : (
          <p className="text-gray-500">Выберите счет для просмотра</p>
        )}
      </div>
    </div>
  );
};

export default Accounts;
