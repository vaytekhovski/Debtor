import transactionSlice from "./transaction-slice";
import { AnyAction, ThunkAction } from "@reduxjs/toolkit";

import { RootState } from "./index";
import { TransactionModel } from "../models/redux-models";
import TransactionService from "../service/transactionService";

export const transactionActions = transactionSlice.actions;

export const fetchTransactions = (): ThunkAction<
  void,
  RootState,
  unknown,
  AnyAction
> => {
  return async (dispath, getState) => {
    if (getState().transaction.all_transactions.length === 0) {
      const response: TransactionModel[] =
        await TransactionService.getAllTransactions();
      dispath(transactionActions.setTransactions(response));
    }
  };
};

export const fetchParticularTransaction = (
  transaction_id: number
): ThunkAction<void, RootState, unknown, AnyAction> => {
  return async (dispatch, getState) => {
    const response: TransactionModel =
      await TransactionService.getParticularTransaction(transaction_id);
    dispatch(transactionActions.setParticularTransaction(response));
  };
};
