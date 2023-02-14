import "./TransactionList.scss";
import { useAppDispatch, useAppSelector } from "../../../../hooks/redux-hooks";
import {
  fetchTransactions,
  fetchParticularTransaction,
} from "../../../../store/transaction-actions";
import { useState, useEffect } from "react";

const TransactionList = () => {
  const [transactionId, setTransactionId] = useState(0);
  const dispatch = useAppDispatch();
  const [skeletonTransactions, setSkeletonTransactions] = useState([
    1, 2, 3, 3, 1, 2, 3, 3, 1, 2, 3, 3, 1, 2,
  ]);

  const allTransactions = useAppSelector(
    (state) => state.transaction.all_transactions
  );

  const particularTransaction = useAppSelector(
    (state) => state.transaction.particular_transaction
  );

  const searchHandler = () => {
    dispatch(fetchParticularTransaction(transactionId));
  };

  const checkTransactions = (): boolean => {
    return allTransactions.length !== 0;
  };

  const handleTransactionClick = (transactionId: number) => {
    setTransactionId(transactionId);
    searchHandler();
  };

  useEffect(() => {
    dispatch(fetchTransactions());
  }, []);

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
                id={transaction.id.toString()}
                className={`transaction-element ${
                  transaction.id === transactionId ? `particular` : `basic`
                }`}
              >
                <span>{transaction.id}</span>
                <span>{transaction.name}</span>
                <span>{transaction.amount}</span>
                <span>{transaction.status}</span>
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
