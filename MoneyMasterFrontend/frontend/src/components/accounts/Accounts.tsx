import { useEffect, useState, useCallback } from "react";
import { useAccountContext } from "@/Context/AccountContext";
import { accountService } from "@/services/accountService";
import { AccountCreateInfo, AccountResponse, UpdatingAccountRequest } from "@/types";
import AccountDetails from "./AccountDetails";
import AccountList from "./AccountList";
import AccountAddModal from "@components/accounts/AccountAddModal";
import { useTransactionContext } from "@/Context/TransactionContext";

const AccountListSkeleton = () => (
  <div className="space-y-2">
    {[...Array(5)].map((_, i) => (
      <div key={i} className="animate-pulse bg-gray-200 rounded-lg h-16"></div>
    ))}
  </div>
);

const AccountDetailsSkeleton = () => (
  <div className="space-y-4">
    <div className="animate-pulse bg-gray-200 rounded h-8 w-1/2"></div>
    <div className="animate-pulse bg-gray-200 rounded h-6 w-3/4"></div>
    <div className="animate-pulse bg-gray-200 rounded h-4 w-full"></div>
    <div className="animate-pulse bg-gray-200 rounded h-4 w-5/6"></div>
    <div className="animate-pulse bg-gray-200 rounded h-12 w-24 mt-4"></div>
  </div>
);

const Accounts: React.FC = () => {
  const { 
    accounts, 
    loading, 
    error, 
    addAccount, 
    selectedAccount, 
    selectAccount, 
    deleteAccount, 
    fetchAccounts,
    updateAccount
  } = useAccountContext();
  
  const { transactions } = useTransactionContext();
  const [isAccountAddModalOpen, setIsAccountAddModalOpen] = useState<boolean>(false);
  const [accountCreateInfo, setAccountCreateInfo] = useState<AccountCreateInfo | null>(null);

const updateAccountBalances = useCallback(async () => {
  try {
    // Получаем актуальные данные по счетам
    const response = await accountService.getAccounts();
    const updatedAccounts: AccountResponse[] = response.data;
    
    updatedAccounts.forEach((updatedAccount: AccountResponse) => {
      const existingAccount = accounts.find(a => a.id === updatedAccount.id);
      if (existingAccount && existingAccount.balance !== updatedAccount.balance) {
        updateAccount(updatedAccount);
      }
    });
  } catch (error) {
    console.error("Ошибка при обновлении балансов счетов:", error);
  }
}, [accounts, updateAccount]);

  const handleGetCreateInfo = useCallback(async () => {
    try {
      const createInfo = await accountService.getCreateInfo();
      setAccountCreateInfo(createInfo);
    } catch (error) {
      console.error("Ошибка при получении информации для создания счета:", error);
    }
  }, []);

  const handleDeleteAccount = useCallback(async (accountId: string) => {
    try {
      await accountService.deleteAccount(accountId);
      deleteAccount(accountId);
      if (selectedAccount?.id === accountId) {
        selectAccount(null);
      }
    } catch (error) {
      console.error("Ошибка при удалении счета:", error);
    }
  }, [deleteAccount, selectAccount, selectedAccount]);

  const handleUpdateAccount = useCallback(async (updatedAccount: UpdatingAccountRequest) => {
    try {
      const response = await accountService.updateAccount(updatedAccount);
      if (response) updateAccount(response);
    } catch (error) {
      console.error("Ошибка при обнолении счета:", error);
    }
  }, [updateAccount, selectAccount, selectedAccount]);

  useEffect(() => {
    const loadData = async () => {
      await Promise.all([
        fetchAccounts(),
        handleGetCreateInfo()
      ]);
    };
    loadData();
  }, [fetchAccounts, handleGetCreateInfo]);

  useEffect(() => {
    if (transactions.length > 0) {
      updateAccountBalances();
    }
  }, [transactions, updateAccountBalances]);

  if (loading) return (
    <div className="p-4 flex w-full">
      <div className="w-1/6 h-screen overflow-y-auto relative">
        <div className="sticky top-0 z-20 space-y-4">
          <div className="animate-pulse bg-gray-200 rounded h-8 w-3/4"></div>
          <div className="animate-pulse bg-gray-200 rounded h-10 w-full"></div>
          <AccountListSkeleton />
        </div>
      </div>
      <div className="pl-6 w-full">
        <AccountDetailsSkeleton />
      </div>
    </div>
  );

  if (error) return <div className="p-4 text-red-500">Ошибка: {error}</div>;

  return (
    <div className="p-4 flex w-full">
      <div className="w-1/6 h-screen overflow-y-auto relative">
        <div className="sticky top-0 z-20">
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
            addAccount(newAccount)
          }
        />

        <AccountList
          accounts={accounts}
          accountTypes={accountCreateInfo?.accountTypes || []}
          selectedAccount={selectedAccount}
          setSelectedAccount={selectAccount}
        />
        </div>
      </div>

      <div className="pl-6 w-full">
        {selectedAccount ? (
          <AccountDetails
            selectedAccount={selectedAccount}
            accountTypes={accountCreateInfo?.accountTypes || []}
            onDeleteAccount={handleDeleteAccount}
            onUpdateAccount={handleUpdateAccount}
          />
        ) : (
          <p className="text-gray-500">Выберите счет для просмотра</p>
        )}
      </div>
    </div>
  );
};

export default Accounts;