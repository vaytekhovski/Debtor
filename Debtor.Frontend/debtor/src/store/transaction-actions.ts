import transactionSlice from "./transaction-slice";
import { AnyAction, ThunkAction } from "@reduxjs/toolkit";

import { RootState } from "./index";
import { TransactionModel } from "../models/redux-models";
import TransactionService from "../service/transactionService";

export const transactionActions = transactionSlice.actions;

export const fetchTransactions = (
  userId: string | undefined
): ThunkAction<void, RootState, unknown, AnyAction> => {
  return async (dispath) => {
    console.log("Trying to fetch transactions, userId: ", userId);
    if (userId !== undefined) {
      console.log("Fetching transactions, userId: ", userId);
      const response: TransactionModel[] | string =
        (await TransactionService.getAllTransactions(userId)) as
          | TransactionModel[]
          | string;
      if (typeof response !== "string") {
        dispath(transactionActions.setTransactions(response));
      } else {
        console.log("Fetching transactions failed, error: ", response);
      }
    } else {
      console.log("Failed to fetch transactions, userId: ", userId);
    }
  };
};

export const postTransactions = (
  transaction: TransactionModel
): ThunkAction<void, RootState, unknown, AnyAction> => {
  return async (dispath) => {
    console.log("Trying to post transaction", transaction);
    if (transaction !== undefined) {
      console.log("Posting transaction", transaction);
      const response: TransactionModel =
        (await TransactionService.postTransaction(
          transaction
        )) as TransactionModel;
      dispath(transactionActions.createTransaction(response));
    } else {
      console.log("Failed to post transaction:", transaction);
    }
  };
};
