import Api from "./Api";
import { TransactionModel } from "../models/redux-models";

export default {
  async getAllTransactions() {
    const response = await Api().get("transactions");
    return response.data;
  },
  async getParticularTransaction(transaction_id: number) {
    const response = await Api().get("transactions");
    return response.data.filter(
      (transaction: TransactionModel) => transaction.id === transaction_id
    )[0];
  },
};
