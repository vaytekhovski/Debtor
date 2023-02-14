export interface TransactionModel {
  id: number;
  name: string;
  amount: number;
  date: Date;
  description: string;
  status: string;
}

export interface TransactionArrayModel {
  all_transactions: TransactionModel[];
  particular_transaction: TransactionModel;
}
