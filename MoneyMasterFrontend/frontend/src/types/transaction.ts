export interface TransactionResponse {
  id: string;
  amount: number;
  categoryId: string;
  description: string;
  accountId: string;
  createAt: string;
}

export interface CreateTransactionRequest {
  amount: number;
  categoryId: string;
  description: string;
  accountId: string;
}

export interface CreateTransactionTransferRequest {
  amount: number;
  fromAccountId: string;
  fromCategoryId: string;
  toAccountId: string;
  toCategoryId: string;
  description: string;
}

export interface UpdateTransactionRequest {
  amount: number;
  categoryId: string;
  description: string;
  accountId: string;
}
