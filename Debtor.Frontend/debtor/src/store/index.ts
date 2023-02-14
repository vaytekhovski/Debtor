import { configureStore } from "@reduxjs/toolkit";
import transactionSlice from "./transaction-slice";

const store = configureStore({
  reducer: { transaction: transactionSlice.reducer },
});

export type RootState = ReturnType<typeof store.getState>;
export type AppDispatch = typeof store.dispatch;
export default store;
