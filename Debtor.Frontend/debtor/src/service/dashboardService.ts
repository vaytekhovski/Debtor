import Api, { getAsync } from "./Api";

export default {
  async getDashboard(user_id: string) {
    return await getAsync(`dashboard/${user_id}`);
  },
};
