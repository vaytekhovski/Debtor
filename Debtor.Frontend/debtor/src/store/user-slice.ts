import { UserModel } from "../models/redux-models";
import { createSlice, PayloadAction } from "@reduxjs/toolkit";
import { act } from "@testing-library/react";

const initialUserState: UserModel = {
  id: undefined,
  name: "default",
  login: "",
  friends: [],
};

const userSlice = createSlice({
  name: "user",
  initialState: initialUserState,
  reducers: {
    setUser(state, action: PayloadAction<UserModel>) {
      state.id = action.payload.id;
      state.name = action.payload.name;
      state.login = action.payload.login;
      state.creatorId = action.payload.creatorId;
      state.friends = action.payload.friends;
      state.error = action.payload.error;
    },
    createNewUser(state, action: PayloadAction<UserModel>) {
      state.friends?.push(action.payload);
      state.error = action.payload.error;
    },
    setUserId(state, action: PayloadAction<string>) {
      state.id = action.payload;
    },
    setUserFriends(state, action: PayloadAction<UserModel[]>) {
      state.friends = action.payload;
    },
  },
});
export default userSlice;
