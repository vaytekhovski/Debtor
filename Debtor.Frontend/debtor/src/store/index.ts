import { configureStore } from "@reduxjs/toolkit";
import dashboardSlice from "./dashboard-slice";
import transactionSlice from "./transaction-slice";
import userSlice from "./user-slice";

const store = configureStore({
  reducer: {
    transaction: transactionSlice.reducer,
    user: userSlice.reducer,
    dashboard: dashboardSlice.reducer,
  },
});

export type RootState = ReturnType<typeof store.getState>;
export type AppDispatch = typeof store.dispatch;
export default store;
