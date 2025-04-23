import { TransactionResponse } from "@/types";
import { CategoryResponse } from "@/types";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { formatCurrency } from "@/utils/format";
import {
  faPlus,
  faMinus,
  faRightLeft,
  faFilter,
  faEllipsisH,
} from "@fortawesome/free-solid-svg-icons";
import { format, isToday, isYesterday, parseISO } from "date-fns";
import { ru } from "date-fns/locale";

interface TransactionHistoryProps {
  transactions: TransactionResponse[];
  categories: CategoryResponse[];
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

const formatGroupDate = (dateStr: string) => {
  const date = parseISO(dateStr);

  if (isToday(date)) {
    return `Сегодня, ${format(date, "d MMMM", { locale: ru })}`;
  }

  if (isYesterday(date)) {
    return `Вчера, ${format(date, "d MMMM", { locale: ru })}`;
  }

  return format(date, "d MMMM", { locale: ru });
};

const groupByDate = (transactions: TransactionResponse[]) => {
  const groups: { [date: string]: TransactionResponse[] } = {};

  transactions.forEach((tx) => {
    const date = tx.createAt.split("T")[0]; // предполагается ISO строка
    if (!groups[date]) groups[date] = [];
    groups[date].push(tx);
  });

  return Object.entries(groups).sort((a, b) => (a[0] < b[0] ? 1 : -1));
};

const TransactionHistory: React.FC<TransactionHistoryProps> = ({
  transactions,
  categories,
}) => {
  const grouped = groupByDate(transactions);

  return (
    <div className="mt-4 pb-4 bg-white rounded-md shadow-md overflow-hidden">
      <h3 className="p-4 pb-2 text-lg font-medium border-1 border-white border-b-gray-200 mb-2 flex items-center justify-between">
        История
        <FontAwesomeIcon
          icon={faFilter}
          className="text-2xl text-gray-700 transaction-transform duration-300 hover:scale-120"
        />
      </h3>
      <ul className="text-gray-600 space-y-3">
        {transactions.length > 0 ? (
          grouped.map(([date, txList], index) => (
            <li key={date}>
              <div className="px-4 pb-1 text-lg text-gray-600 border-2 text-center border-white border-b-gray-200 ">
                {formatGroupDate(date)}
              </div>
              <ul className="space-y-1">
                {txList.map((tx) => {
                  const category = categories.find(
                    (category) => category.id === tx.categoryId
                  );
                  const { sign } = getVisuals(category?.categoryType);

                  return (
                    <li
                      key={tx.id}
                      className="px-4 py-2 flex items-center hover:bg-gray-200 cursor-pointer odd:bg-gray-50"
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
          ))
        ) : (
          <li className="p-4 text-center">Транзакций пока нет</li>
        )}
      </ul>
    </div>
  );
};

export default TransactionHistory;
