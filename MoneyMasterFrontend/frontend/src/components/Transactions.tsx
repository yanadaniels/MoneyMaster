import { useEffect, useState } from "react";
import { useAuth } from "../Context/AuthContext";
import authService from "../services/authService";
import { transactionService } from "../services/transactionService";

interface Transaction {
  id: string;
  amount: number;
  categoryId: string;
  description: string;
  accountId: string;
  createAt: string;
}

const Transactions: React.FC = () => {
  const { state } = useAuth();
  const [transactions, setTransactions] = useState<Transaction[]>([]);
  const [loading, setLoading] = useState<boolean>(true);

  useEffect(() => {
    const fetchTransactions = async () => {
      try {
        if (!state.user) return;
        const accessToken = authService.getToken();
        if (!accessToken) return;

        const data = await transactionService.getTransactions(accessToken);
        setTransactions(data);
      } catch (error) {
        console.error("Ошибка при запуске транзакции:", error);
      } finally {
        setLoading(false);
      }
    };

    fetchTransactions();
  }, [state.user]);

  if (loading) return <p>Загрузка...</p>;

  return (
    <div className="p-4">
      <h2 className="text-xl font-bold mb-4">Мои транзакции</h2>
      {transactions.length === 0 ? (
        <p className="text-gray-500">Транзакций пока нет.</p>
      ) : (
        <ul className="bg-white shadow-md rounded-lg p-4">
          {transactions.map((transaction) => (
            <li key={transaction.id} className="border-b last:border-none p-2">
              <span className="font-semibold">{transaction.categoryId}</span>:{" "}
              {transaction.amount} ₽ (
              {new Date(transaction.createAt).toLocaleDateString()})
            </li>
          ))}
        </ul>
      )}
    </div>
  );
};

export default Transactions;
