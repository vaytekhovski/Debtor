import { UserModel } from "../models/redux-models";
import { getAsync, postAsync } from "./Api";

export default {
  async getUser(user_id: string) {
    return await getAsync(`user/${user_id}`);
  },
  async postUser(user: UserModel) {
    return await postAsync(`user`, user);
  },
  async getUserFriends(user_id: string) {
    return await getAsync(`user/${user_id}/friends`);
  },
};
