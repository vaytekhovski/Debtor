import React, { useEffect } from "react";

import Dashboard from "./Dashboard/Dashboard";
import TransactionList from "./TransactionList/TransactionList";
import { useAppDispatch, useAppSelector } from "../../../hooks/redux-hooks";
import {
  fetchUser,
  fetchUserFriends,
  setUserId,
} from "../../../store/user-actions";
import { fetchTransactions } from "../../../store/transaction-actions";
import { fetchDashboard } from "../../../store/dashboard.actions";
import store from "../../../store";

const HomePage = () => {
  const dispatch = useAppDispatch();

  const loadData = async () => {
    console.log("*************loading data");
    dispatch(setUserId(process.env.REACT_APP_MY_TEST_USER_ID!));
    const User = store.getState().user;
    console.log("User");
    if (User.id !== undefined) {
      dispatch(fetchUser(User.id));
      dispatch(fetchDashboard(User.id));
      dispatch(fetchTransactions(User.id));
      dispatch(fetchUserFriends(User.id));
    }
    console.log("loading data complete");
  };

  useEffect(() => {
    try {
      loadData().then(() => console.log("Loaded"));
    } catch (error) {
      console.log("Error:", error);
    }
  }, []);

  return (
    <>
      <Dashboard />
      <TransactionList />
    </>
  );
};

export default HomePage;
