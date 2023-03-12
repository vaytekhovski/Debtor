export interface UserFromModel {
  id: string;
  name: string;
  login?: any;
}

export interface UserToModel {
  id: string;
  name: string;
  login?: any;
}

export interface DebtorModel {
  error?: string;
}

export interface TransactionModel extends DebtorModel {
  id: string;
  userFromId: string;
  userFrom?: UserFromModel;
  userToId: string;
  userTo?: UserToModel;
  jointId?: any;
  joint?: any;
  amount: number;
  description: string;
  status?: string;
  type?: string;
  date?: string;
}

export interface UserModel extends DebtorModel {
  id?: string;
  creatorId?: string;
  name: string;
  login?: string;
  friends?: UserModel[];
}

export interface TransactionArrayModel extends DebtorModel {
  all_transactions: TransactionModel[];
  particular_transaction: TransactionModel;
}
export interface DashboardModel extends DebtorModel {
  incomingAmount: string;
  outcomingAmount: string;
}
