import { TransactionModel } from "../models/redux-models";
import Api, { getAsync, postAsync } from "./Api";

export default {
  async getAllTransactions(userId: string) {
    return await getAsync(`transaction/${userId}`);
  },
  async postTransaction(transaction: TransactionModel) {
    return await postAsync(`transaction`, transaction);
  },
};
