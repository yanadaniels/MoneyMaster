import { useState, useEffect, useMemo } from "react";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { formatCurrency, formatGroupDate, groupByDate } from "@/utils/format";
import { faPlus, faMinus, faRightLeft, faEllipsisH } from "@fortawesome/free-solid-svg-icons";
import { AccountResponse, TransactionResponse, UpdateTransactionRequest } from "@/types";
import { transactionService } from "@/services/transactionService";
import UpdateModal from "./UpdateModal";
import { useTransactionContext } from "@/Context/TransactionContext";
import { useCategoryContext } from "@/Context/CategoryContext";

const TransactionItemSkeleton = () => (
  <li className="px-4 py-2 flex items-center odd:bg-gray-50">
    <div className="animate-pulse rounded-full bg-gray-200 w-10 h-10"></div>
    <div className="flex flex-col ml-5 space-y-2">
      <div className="animate-pulse bg-gray-200 h-4 w-20 rounded"></div>
      <div className="animate-pulse bg-gray-200 h-3 w-16 rounded"></div>
    </div>
    <div className="animate-pulse bg-gray-200 w-6 h-6 ml-auto rounded-full"></div>
  </li>
);

const DateGroupSkeleton = () => (
  <li>
    <div className="animate-pulse px-4 pb-1 h-6 bg-gray-200 w-32 mx-auto mb-2 rounded"></div>
    <ul className="space-y-1">
      {[...Array(3)].map((_, i) => (
        <TransactionItemSkeleton key={i} />
      ))}
    </ul>
  </li>
);

const PaginationSkeleton = () => (
  <div className="flex items-center justify-between mt-4 px-4">
    <div className="animate-pulse bg-gray-200 h-8 w-16 rounded"></div>
    <div className="animate-pulse bg-gray-200 h-4 w-24 rounded"></div>
    <div className="animate-pulse bg-gray-200 h-8 w-16 rounded"></div>
  </div>
);

interface TransactionListProps {
  selectedAccount: AccountResponse;
  filter: {
    startDate: Date | null;
    endDate: Date | null;
  };
}

const isRevenue = (type?: string) => type === "Revenue";
const isExpense = (type?: string) => type === "Expenses";
const isTransfer = (type?: string) => type === "Transfer";

const getVisuals = (categoryType?: string) => {
  if (isRevenue(categoryType))
    return {
      sign: (
        <FontAwesomeIcon icon={faPlus} className="text-2xl text-green-400" />
      ),
      colorClass: "text-green-600",
    };
  if (isExpense(categoryType))
    return {
      sign: (
        <FontAwesomeIcon icon={faMinus} className="text-2xl text-red-400" />
      ),
      colorClass: "text-red-600",
    };
  if (isTransfer(categoryType))
    return {
      sign: (
        <FontAwesomeIcon
          icon={faRightLeft}
          className="text-2xl text-yellow-400"
        />
      ),
      colorClass: "text-yellow-600",
    };
  return { sign: "", colorClass: "text-gray-800" };
};

const TransactionsList: React.FC<TransactionListProps> = ({ selectedAccount, filter }) => {
  const [currentPage, setCurrentPage] = useState(1);
  const [limit] = useState(10);
  const [isUpdateModalOpen, setIsUpdateModalOpen] = useState(false);
  const [needUpdateTransaction, setNeedUpdateTransaction] = useState<TransactionResponse | null>(null);
  const [showSkeleton, setShowSkeleton] = useState(true);
  const { transactions, fetchTransactions, updateTransaction, loading, error } = useTransactionContext();
  const { categories } = useCategoryContext();

  useEffect(() => {
      let timeoutId: NodeJS.Timeout;
      
      if (loading) {
        setShowSkeleton(true);
      } else {
        // Добавляем задержку перед скрытием скелетона
        timeoutId = setTimeout(() => setShowSkeleton(false), 300);
      }
      
      return () => clearTimeout(timeoutId);
    }, [loading]);

  const filteredTransactions = useMemo(() => {
    if (!filter.startDate && !filter.endDate) return transactions;
    
    return transactions.filter(tx => {
      const txDate = new Date(tx.createAt);
      return (
        (!filter.startDate || txDate >= filter.startDate) &&
        (!filter.endDate || txDate <= filter.endDate)
      );
    });
  }, [transactions, filter.startDate, filter.endDate]);

  const totalPages = Math.ceil(filteredTransactions.length / limit);
  const currentItems = filteredTransactions.slice(
    (currentPage - 1) * limit,
    currentPage * limit
  );
  const grouped = groupByDate(currentItems);

  useEffect(() => {
    const loadData = async () => {
      await fetchTransactions(selectedAccount.id);
    };
    loadData();
  }, [selectedAccount.id, fetchTransactions]);

  const handleUpdateTransaction = async (transactionId: string, data: UpdateTransactionRequest) => {
    const response = await transactionService.updateTransaction(transactionId, data);
    if (response) await updateTransaction(response);
    setIsUpdateModalOpen(false);
  };

  const handleDeleteTransaction = async (transactionId: string) => {
    await transactionService.deleteTransactionById(transactionId);
    await fetchTransactions(selectedAccount.id);
    setIsUpdateModalOpen(false);
  };

  const handleClickTransaction = (tx: TransactionResponse) => {
    setNeedUpdateTransaction(tx);
    setIsUpdateModalOpen(true);
  };

  const handlePrevPage = () => {
    if (currentPage > 1) setCurrentPage(prev => prev - 1);
  };

  const handleNextPage = () => {
    if (currentPage < totalPages) setCurrentPage(prev => prev + 1);
  };

  if (showSkeleton) return (
    <ul className="text-gray-600 space-y-3">
      {[...Array(1)].map((_, i) => (
        <DateGroupSkeleton key={i} />
      ))}
      <PaginationSkeleton />
    </ul>
  );

  if (error) return <div className="p-4 text-red-500">Ошибка: {error}</div>;

  return (
    <ul className="text-gray-600 space-y-3">
      {filteredTransactions.length > 0 ? (
        <>
          {grouped.map(([date, txList]) => (
            <li key={date}>
              <div className="px-4 pb-1 text-lg text-gray-600 border-2 text-center border-white border-b-gray-200">
                {formatGroupDate(date)}
              </div>
              <ul className="space-y-1">
                {txList.map((tx) => {
                  const category = categories.find(c => c.id === tx.categoryId);
                  const { sign } = getVisuals(category?.categoryType);

                  return (
                    <li
                      key={tx.id}
                      className="px-4 py-2 flex items-center hover:bg-gray-200 cursor-pointer odd:bg-gray-50"
                      onClick={() => handleClickTransaction(tx)}
                    >
                      <div className="rounded-full bg-gray-200 w-10 h-10 flex items-center justify-center">
                        {sign}
                      </div>
                      <div className="flex flex-col ml-5">
                        <span className="font-bold">
                          {formatCurrency(tx.amount)}
                        </span>
                        <span className="text-sm text-gray-500">
                          {category?.name || "Без категории"}
                        </span>
                      </div>
                      <FontAwesomeIcon
                        icon={faEllipsisH}
                        className="text-2xl text-gray-400 ml-auto"
                      />
                    </li>
                  );
                })}
              </ul>
            </li>
          ))}

          <div className="flex items-center justify-between mt-4 px-4">
            <button
              onClick={handlePrevPage}
              disabled={currentPage === 1}
              className={`px-4 py-2 rounded ${currentPage === 1 ? 'bg-gray-200 cursor-not-allowed' : 'bg-blue-500 text-white hover:bg-blue-600'}`}
            >
              Назад
            </button>
            <span className="text-sm">
              Страница {currentPage} из {totalPages}
            </span>
            <button
              onClick={handleNextPage}
              disabled={currentPage === totalPages}
              className={`px-4 py-2 rounded ${currentPage === totalPages ? 'bg-gray-200 cursor-not-allowed' : 'bg-blue-500 text-white hover:bg-blue-600'}`}
            >
              Вперед
            </button>
          </div>

          <UpdateModal
            isOpen={isUpdateModalOpen}
            onClose={() => setIsUpdateModalOpen(false)}
            selectedAccont={selectedAccount}
            transaction={needUpdateTransaction}
            categories={categories}
            onConfirm={handleUpdateTransaction}
            onDelete={handleDeleteTransaction}
          />
        </>
      ) : (
        <li className="p-4 text-center">Транзакций пока нет</li>
      )}
    </ul>
  );
};

export default TransactionsList;