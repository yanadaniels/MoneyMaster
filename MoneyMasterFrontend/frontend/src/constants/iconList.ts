// src/constants/iconList.ts
import {
  faWallet,
  faPiggyBank,
  faCreditCard,
  faMoneyBillWave,
  faCoins,
  faLandmark,
  IconDefinition,
} from "@fortawesome/free-solid-svg-icons";

export const iconMap: Record<string, IconDefinition> = {
  faWallet,
  faPiggyBank,
  faCreditCard,
  faMoneyBillWave,
  faCoins,
  faLandmark,
};

export const iconOptions = Object.keys(iconMap); // ["faWallet", "faPiggyBank", ...]
