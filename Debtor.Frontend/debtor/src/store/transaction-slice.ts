import {
  TransactionModel,
  TransactionArrayModel,
} from "../models/redux-models";
import { createSlice, PayloadAction } from "@reduxjs/toolkit";

const initialTransactionState: TransactionArrayModel = {
  all_transactions: [],
  particular_transaction: {
    id: 0,
    name: "",
    date: new Date(),
    amount: 0,
    description: "",
    status: "",
  },
};

const transactionSlice = createSlice({
  name: "transaction",
  initialState: initialTransactionState,
  reducers: {
    setTransactions(state, action: PayloadAction<TransactionModel[]>) {
      state.all_transactions = action.payload;
    },
    setParticularTransaction(state, action: PayloadAction<TransactionModel>) {
      state.particular_transaction = action.payload;
    },
  },
});
export default transactionSlice;
