import { useEffect, useState, useRef } from "react";
import { useAuth } from "@/Context/AuthContext";
import { useAccountContext } from "@/Context/AccountContext";
import { accountService } from "@/services/accountService";
import { AccountResponse, AccountCreateInfo } from "@/types";
import AccountDetails from "./AccountDetails";
import AccountList from "./AccountList";
import AccountAddModal from "@components/accounts/AccountAddModal";

const Accounts: React.FC = () => {
  const { state } = useAuth();
  const { accounts, setAccounts } = useAccountContext();
  const [loading, setLoading] = useState<boolean>(true);
  const [isAccountAddModalOpen, setIsAccountAddModalOpen] =
    useState<boolean>(false);
  const [accountCreateInfo, setAccountCreateInfo] =
    useState<AccountCreateInfo | null>(null);
  const isFetched = useRef(false);
  const [selectedAccount, setSelectedAccount] =
    useState<AccountResponse | null>(null);

  useEffect(() => {
    const fetchAccounts = async () => {
      try {
        // if (!state.user || isFetched.current) return; // Проверяем, что пользователь загружен и не было запроса
        // isFetched.current = true; // Устанавливаем флаг, что запрос уже был выполнен

        const data = await accountService.getAccounts();
        setAccounts(data.data);
        handleGetCreateInfo();
      } catch (error) {
        console.error("Ошибка при запуске счетов:", error);
      } finally {
        setLoading(false);
      }
    };

    fetchAccounts();
  }, []);

  const handleGetCreateInfo = async () => {
    try {
      const createInfo = await accountService.getCreateInfo();
      setAccountCreateInfo(createInfo);
    } catch (error) {
      console.error("Ошибка при получении информации для создания счета");
    }
  };

  const updateAccountState = (updatedAccount: AccountResponse) => {
    setAccounts((prevAccounts) =>
      prevAccounts.map((acc) =>
        acc.id === updatedAccount.id ? updatedAccount : acc
      )
    );
    setSelectedAccount(updatedAccount);
  };

  const handleDeleteAccount = async (accountId: string) => {
    try {
      await accountService.deleteAccount(accountId);
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

        <button
          onClick={() => setIsAccountAddModalOpen(true)}
          className="bg-blue-500 text-white px-4 py-2 rounded mb-4 hover:bg-blue-600 cursor-pointer"
        >
          Добавить новый счет
        </button>

        <AccountAddModal
          isOpen={isAccountAddModalOpen}
          onClose={() => setIsAccountAddModalOpen(false)}
          accountCreateInfo={accountCreateInfo}
          onCreateAccount={(newAccount) =>
            setAccounts((prevAccounts) => [...prevAccounts, newAccount])
          }
        />

        <AccountList
          accounts={accounts}
          accountTypes={accountCreateInfo?.accountTypes || []}
          selectedAccount={selectedAccount}
          setSelectedAccount={setSelectedAccount}
          handleDeleteAccount={handleDeleteAccount}
        />
      </div>

      <div className="pl-6 w-full">
        {selectedAccount ? (
          <AccountDetails
            account={selectedAccount}
            accountTypes={accountCreateInfo?.accountTypes || []}
            onAccountUpdate={updateAccountState}
          />
        ) : (
          <p className="text-gray-500">Выберите счет для просмотра</p>
        )}
      </div>
    </div>
  );
};

export default Accounts;
