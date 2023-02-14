import React from "react";
import LoginButton from "../../Buttons/LoginButton";
import LogoutButton from "../../Buttons/LogoutButton";
import Dashboard from "./Dashboard/Dashboard";
import TransactionList from "./TransactionList/TransactionList";
import Toolbar from "./Toolbar/Toolbar";

// eslint-disable-next-line @typescript-eslint/no-empty-interface
export interface IHomePageProps {}

const HomePage: React.FunctionComponent<IHomePageProps> = (props) => {
  return (
    <>
      <LoginButton />
      <LogoutButton />
      <Dashboard />
      <TransactionList />
      <Toolbar />
    </>
  );
};

export default HomePage;
