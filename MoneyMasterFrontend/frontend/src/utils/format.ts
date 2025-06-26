import { TransactionResponse } from "@/types";
import { format, isToday, isYesterday, parseISO } from "date-fns";
import { ru } from "date-fns/locale";
import {
  faPlus,
  faMinus,
  faRightLeft,
  faEllipsisH,
} from "@fortawesome/free-solid-svg-icons";


export const formatCurrency = (amount: number) => {
  return new Intl.NumberFormat("ru-RU", {
    style: "currency",
    currency: "RUB",
  }).format(amount);
};

export const groupByDate = (transactions: TransactionResponse[]) => {
  const groups: { [date: string]: TransactionResponse[] } = {};

  transactions.forEach((tx) => {
    const date = tx.createAt.split("T")[0]; // предполагается ISO строка
    if (!groups[date]) groups[date] = [];
    groups[date].push(tx);
  });

  return Object.entries(groups).sort((a, b) => (a[0] < b[0] ? 1 : -1));
};

export const formatGroupDate = (dateStr: string) => {
  const date = parseISO(dateStr);

  if (isToday(date)) {
    return `Сегодня, ${format(date, "d MMMM", { locale: ru })}`;
  }

  if (isYesterday(date)) {
    return `Вчера, ${format(date, "d MMMM", { locale: ru })}`;
  }

  return format(date, "d MMMM", { locale: ru });
};
