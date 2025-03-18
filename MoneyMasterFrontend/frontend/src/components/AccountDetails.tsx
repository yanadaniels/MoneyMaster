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

interface AccountDetailsProps {
  account: Account;
}

const AccountDetails: React.FC<AccountDetailsProps> = ({ account }) => {
  return (
    <div className="p-4 rounded-lg bg-white shadow-md">
      <h2 className="text-lg font-semibold">{account.name}</h2>
      <p className="text-gray-600">
        Баланс: <strong>{account.balance.toLocaleString()}</strong>
      </p>

      <div className="mt-4">
        <h3 className="text-md font-medium">Операции</h3>
        <button className="mt-2 bg-green-500 text-white px-4 py-2 rounded-lg mr-2 hover:bg-green-600">
          Пополнить
        </button>
        <button className="mt-2 bg-red-500 text-white px-4 py-2 rounded-lg hover:bg-red-600">
          Списать
        </button>
      </div>

      <div className="mt-4">
        <h3 className="text-md font-medium"> История транзакций </h3>
        <ul className="mt-2 text-gray-600">
          <li>+1000 Р (Пополнение) - 03.03.2025</li>
          <li>+500 Р (Оплата) - 01.03.2025</li>
          <li>+3000 Р (Пополнение) - 28.02.2025</li>
        </ul>
      </div>
    </div>
  );
};

export default AccountDetails;
