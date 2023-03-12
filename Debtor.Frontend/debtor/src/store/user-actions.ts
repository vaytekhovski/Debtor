import userSlice from "./user-slice";
import { AnyAction, ThunkAction } from "@reduxjs/toolkit";

import { RootState } from "./index";
import { UserModel } from "../models/redux-models";
import UserService from "../service/userService";

export const userActions = userSlice.actions;

export const fetchUser = (
  userId: string | undefined
): ThunkAction<void, RootState, unknown, AnyAction> => {
  return async (dispatch) => {
    console.log("Trying to fetch user, userId", userId);
    if (userId !== undefined) {
      console.log("Fetching user, userId: ", userId);
      const response: UserModel = (await UserService.getUser(
        userId
      )) as UserModel;
      dispatch(userActions.setUser(response));
    } else {
      console.log("Failed to fetch user, userId: ", userId);
    }
  };
};

export const fetchUserFriends = (
  userId: string | undefined
): ThunkAction<void, RootState, unknown, AnyAction> => {
  return async (dispatch) => {
    console.log("Trying to fetch user friends, userId", userId);
    if (userId !== undefined) {
      console.log("Fetching user friends, userId: ", userId);
      const response: UserModel[] = (await UserService.getUserFriends(
        userId
      )) as UserModel[];
      dispatch(userActions.setUserFriends(response));
    } else {
      console.log("Failed to fetch user friends, userId: ", userId);
    }
  };
};

export const postUser = (
  user: UserModel
): ThunkAction<void, RootState, unknown, AnyAction> => {
  return async (dispath) => {
    console.log("Trying to post user", user);
    if (user.name !== "") {
      console.log("Posting user: ", user);
      const response: UserModel = (await UserService.postUser(
        user
      )) as UserModel;
      dispath(userActions.createNewUser(response));
    } else {
      console.log("Failed to post user, user name: ", user.name);
    }
  };
};

export const setUserId = (
  userId: string | undefined
): ThunkAction<void, RootState, unknown, AnyAction> => {
  return async (dispath) => {
    console.log("Trying to set userId", userId);
    if (userId !== undefined) {
      console.log("Setting userId: ", userId);
      dispath(userActions.setUserId(userId));
    } else {
      console.log("Failed to set userId, userId: ", userId);
    }
  };
};
