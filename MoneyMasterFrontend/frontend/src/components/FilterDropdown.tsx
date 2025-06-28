import React, { useState, useRef, useEffect } from 'react';
import DatePicker from 'react-datepicker';
import 'react-datepicker/dist/react-datepicker.css';
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import {
  faFilter,
} from "@fortawesome/free-solid-svg-icons";

type FilterDropdownProps = {
  onApply: (startDate: Date | null, endDate: Date | null) => void;
  initialDates?: { startDate: Date | null; endDate: Date | null };
};

const FilterDropdown: React.FC<FilterDropdownProps> = ({ onApply, initialDates }) => {
  const [isOpen, setIsOpen] = useState(false);
  const [startDate, setStartDate] = useState<Date | null>(initialDates?.startDate || null);
  const [endDate, setEndDate] = useState<Date | null>(initialDates?.endDate || null);
  const dropdownRef = useRef<HTMLDivElement>(null);

  // Закрытие dropdown при клике вне его области
  useEffect(() => {
    const handleClickOutside = (event: MouseEvent) => {
      if (dropdownRef.current && !dropdownRef.current.contains(event.target as Node)) {
        setIsOpen(false);
      }
    };
    document.addEventListener('mousedown', handleClickOutside);
    return () => document.removeEventListener('mousedown', handleClickOutside);
  }, []);

  const handleApply = () => {
    if (startDate && endDate && startDate > endDate) {
      alert('Дата "От" не может быть позже даты "До"');
      return;
    }
    onApply(startDate, endDate);
    setIsOpen(false);
  };

  const handleReset = () => {
    setStartDate(null);
    setEndDate(null);
    onApply(null, null);
  };

  return (
    <div className="relative inline-block" ref={dropdownRef}>
        <FontAwesomeIcon
            icon={faFilter}
            className="text-2xl text-gray-700 transaction-transform duration-300 hover:scale-120"
            onClick={() => setIsOpen(!isOpen)}
        />
          <div className={`absolute right-0 mt-2 w-84 z-10 bg-white dark:bg-gray-900 rounded-2xl shadow-lg p-4 transform transition-all duration-100 ease-in-out origin-top-right ${
            isOpen
              ? "scale-100 opacity-100"
              : "scale-50 opacity-0"
          }`}>    
          <div className="p-4 space-y-4 select-none">
            <h3 className="text-xl font-semibold mb-4">Фильтр по дате</h3>
            <div className="flex justify-between">
              <div className="mr-5">
                <DatePicker
                  selected={startDate}
                  onChange={setStartDate}
                  selectsStart
                  startDate={startDate}
                  endDate={endDate}
                  dateFormat="dd.MM.yyyy"
                  className="block w-full rounded-md bg-white px-3 py-1.5 text-base text-gray-900 outline-1 outline-gray-300 placeholder:text-gray-400 focus:outline-indigo-600"
                  placeholderText='От'
                />
              </div>
              <div>
                <DatePicker
                  selected={endDate}
                  onChange={setEndDate}
                  selectsEnd
                  startDate={startDate}
                  endDate={endDate}
                  minDate={startDate}
                  dateFormat="dd.MM.yyyy"
                  className="block w-full rounded-md bg-white px-3 py-1.5 text-base text-gray-900 outline-1 outline-gray-300 placeholder:text-gray-400 focus:outline-indigo-600"
                  placeholderText='До'
                />
              </div>
            </div>
            <div className="flex justify-between pt-2">
              <button
                onClick={handleReset}
                className="bg-gray-400 text-white px-4 py-2 rounded w-full"
              >
                Сбросить
              </button>
              <button
                onClick={handleApply}
                className="bg-green-500 text-white px-4 py-2 rounded w-full ml-3"
              >
                Применить
              </button>
            </div>
          </div>
        </div>
    </div>
  );
};

export default FilterDropdown;