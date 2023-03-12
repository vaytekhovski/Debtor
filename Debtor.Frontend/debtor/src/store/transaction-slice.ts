import {
  TransactionModel,
  TransactionArrayModel,
} from "../models/redux-models";
import { createSlice, PayloadAction } from "@reduxjs/toolkit";

const initialTransactionState: TransactionArrayModel = {
  all_transactions: [],
  particular_transaction: {
    id: "",
    userFromId: "",
    userFrom: {
      id: "",
      name: "",
      login: null,
    },
    userToId: "",
    userTo: {
      id: "",
      name: "",
      login: null,
    },
    jointId: null,
    joint: null,
    amount: 0,
    description: "",
    status: "",
    type: "",
    date: "2023-02-16T18:10:11.808Z",
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
    createTransaction(state, action: PayloadAction<TransactionModel>) {
      state.all_transactions.push(action.payload);
    },
  },
});
export default transactionSlice;
