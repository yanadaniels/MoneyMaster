export interface AccountResponse {
  id: string;
  name: string;
  balance: number;
  currency: string;
  icon: string;
  userId: string;
  accountTypeId: string;
  createAt: string;
}

export interface AccountType {
  id: string;
  name: string;
  icon: string;
  isSystem: boolean;
  isDelete: boolean;
  createAt: string;
}

export interface AccountCreateInfo {
  accountTypes: AccountType[];
  currentCode: string[];
}

export interface UpdatingAccountRequest {
  id: string;
  name: string;
  balance: number;
  currency: string;
  icon: string;
  userId: string;
  accountTypeId: string;
}
