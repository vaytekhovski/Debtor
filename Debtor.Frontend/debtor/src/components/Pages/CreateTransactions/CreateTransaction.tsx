import React, { useEffect } from "react";
import CreateTransactionForm from "../../Forms/CreateTransactionForm";
import "./CreateTransaction.scss";
import { useAppDispatch, useAppSelector } from "../../../hooks/redux-hooks";
import { useNavigate } from "react-router-dom";
import { fetchUserFriends } from "../../../store/user-actions";

const CreateTransactionPage = () => {
  const User = useAppSelector((state) => state.user);
  const navigate = useNavigate();

  const dispatch = useAppDispatch();

  useEffect(() => {
    console.log("Create Transaction Page");
    User.id === undefined && navigate("/Home");
    dispatch(fetchUserFriends(process.env.REACT_APP_MY_TEST_USER_ID!));
  }, []);

  return (
    <div className="create-transaction">
      {User.friends && <CreateTransactionForm />}
    </div>
  );
};

export default CreateTransactionPage;
