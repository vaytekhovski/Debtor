import dashboardSlice from "./dashboard-slice";
import { AnyAction, ThunkAction } from "@reduxjs/toolkit";

import { RootState } from "./index";
import { DashboardModel } from "../models/redux-models";
import DashboardService from "../service/dashboardService";

export const dashboardActions = dashboardSlice.actions;

export const fetchDashboard = (
  dashboardId: string
): ThunkAction<void, RootState, unknown, AnyAction> => {
  return async (dispatch, getState) => {
    const state = getState();
    console.log("Trying to fetch dashboard, dashboardId", dashboardId);
    if (state.user.id !== undefined) {
      console.log("Fetching dashboard, userId", state.user.id);
      const response: DashboardModel = (await DashboardService.getDashboard(
        dashboardId
      )) as DashboardModel;
      dispatch(dashboardActions.setDashboard(response));
    } else {
      console.log(
        `Fail to fetch dashboard dashboardId: ${dashboardId}, userId: ${state.user.id}`
      );
    }
  };
};
