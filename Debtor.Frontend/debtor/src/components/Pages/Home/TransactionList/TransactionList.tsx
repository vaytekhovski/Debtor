import "./TransactionList.scss";
import { useAppDispatch, useAppSelector } from "../../../../hooks/redux-hooks";
import { fetchTransactions } from "../../../../store/transaction-actions";
import { useState, useEffect } from "react";

const TransactionList = () => {
  const [transactionId, setTransactionId] = useState("");
  const dispatch = useAppDispatch();
  const [skeletonTransactions, setSkeletonTransactions] = useState([
    1, 2, 3, 3, 1, 2, 3, 3, 1, 2, 3, 3, 1, 2,
  ]);

  const allTransactions = useAppSelector(
    (state) => state.transaction.all_transactions
  );

  const User = useAppSelector((state) => state.user);

  const particularTransaction = useAppSelector(
    (state) => state.transaction.particular_transaction
  );

  const checkTransactions = (): boolean => {
    return allTransactions.length !== 0;
  };

  const handleTransactionClick = (transactionId: string) => {
    setTransactionId((prev) => (prev === transactionId ? "" : transactionId));
  };

  useEffect(() => {
    if (User.id !== undefined && allTransactions.length === 0) {
      dispatch(fetchTransactions(User.id!));
    }
  }, [User, allTransactions]);

  return (
    <>
      <div className="title-container">
        <h3>All Transactions</h3>
      </div>

      <div className="transaction-list">
        {checkTransactions() ? (
          allTransactions.map((transaction) => (
            <div
              className="transaction-line"
              key={transaction.id}
              onClick={() => handleTransactionClick(transaction.id)}
            >
              <div
                id={transaction.id}
                className={`transaction-element ${
                  transaction.id === transactionId ? `particular` : `basic`
                } ${transaction.type === "up" ? `up` : `down`}`}
              >
                <span className="name">{transaction.userFrom!.name}</span>
                <span>➜</span>
                <span className="name">{transaction.userTo!.name}</span>
                <span className="">${transaction.amount}</span>
                <span>{transaction.description}</span>
              </div>
            </div>
          ))
        ) : (
          <>
            {skeletonTransactions.map((skeleton, key) => (
              <div className="transaction-line" key={key}>
                <div className="transaction-element skeleton"></div>
              </div>
            ))}
          </>
        )}
      </div>
    </>
  );
};

export default TransactionList;
