import React, { useEffect } from "react";
import "./TransactionComponent.scss";
import { useAppSelector } from "../../hooks/redux-hooks";
import { useParams } from "react-router-dom";

const TransactionComponent = () => {
  const User = useAppSelector((state) => state.user);
  const { id } = useParams();

  useEffect(() => {
    console.log("Transaction Page");
    // User.id === undefined && navigate("/Home");
  }, []);

  return (
    <div className="transaction-component">
      <h1>Transaction id: {id}</h1>
    </div>
  );
};

export default TransactionComponent;
