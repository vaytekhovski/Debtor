import { DashboardModel } from "../models/redux-models";
import { createSlice, PayloadAction } from "@reduxjs/toolkit";

const initialDashboardState: DashboardModel = {
  outcomingAmount: "",
  incomingAmount: "",
};

const dashboardSlice = createSlice({
  name: "dashboard",
  initialState: initialDashboardState,
  reducers: {
    setDashboard(state, action: PayloadAction<DashboardModel>) {
      state.outcomingAmount = action.payload.outcomingAmount;
      state.incomingAmount = action.payload.incomingAmount;
    },
  },
});
export default dashboardSlice;
