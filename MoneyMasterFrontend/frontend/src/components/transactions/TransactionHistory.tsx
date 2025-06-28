import { useState } from "react";
import { AccountResponse } from "@/types";

import DropdownBox from "../FilterDropdown";
import TransactionsList from "./TransactionsList";

interface TransactionHistoryProps {
  selectedAccount: AccountResponse;
}

const TransactionHistory: React.FC<TransactionHistoryProps> = ({
  selectedAccount,
}) => {

  const [filterDates, setIsFilterDates] = useState<{
    startDate: Date | null;
    endDate: Date | null;
  }>({ startDate: null, endDate: null});

  const handleApplyDates = (startDate: Date | null, endDate: Date | null) => {
    setIsFilterDates({ startDate, endDate});
  };

  return (
    <div className="mt-4 pb-4 bg-white rounded-md shadow-md overflow-visible">
      <h3 className="p-4 pb-2 text-lg font-medium border-1 border-white border-b-gray-200 mb-2 flex items-center justify-between">
        История
        <DropdownBox
          onApply={handleApplyDates}
          initialDates={filterDates}
        />
      </h3>
      <TransactionsList
        selectedAccount={selectedAccount}
        filter={filterDates}
      />
    </div>
  );
};

export default TransactionHistory;
