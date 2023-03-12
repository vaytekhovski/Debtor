import axios, { AxiosError } from "axios";
import { DebtorModel } from "../models/redux-models";

export default () => {
  return axios.create({
    baseURL: "https://localhost:7117/",
  });
};

type ServerError = { errorMessage: string };

export const getAsync = async (url: string): Promise<DebtorModel | string> => {
  try {
    console.log(`get ${url}`);
    const response = await axios
      .create({
        baseURL: "https://localhost:7117/",
      })
      .get(url);
    console.log(`get ${url} response: ${response}`);
    return response.data;
  } catch (error) {
    if (axios.isAxiosError(error)) {
      const serverError = error as AxiosError<ServerError>;
      if (serverError && serverError.response) {
        return serverError.response.data.errorMessage;
      }
    }
    console.log(`Failed to get ${url}`);
    console.log(`Error: ${error}`);
    return `Failed to get ${url}, message: ${error}`;
  }
};

export const postAsync = async (url: string, data: any) => {
  try {
    console.log(`post ${url}, data: ${JSON.stringify(data)}`);
    const response = await axios
      .create({
        baseURL: "https://localhost:7117/",
      })
      .post(url, data);
    console.log(`post ${url} response: ${response}`);
    return response.data;
  } catch (error) {
    if (axios.isAxiosError(error)) {
      const serverError = error as AxiosError<ServerError>;
      if (serverError && serverError.response) {
        return serverError.response.data.errorMessage;
      }
    }
    console.log(`Failed to post ${url}`);
    console.log(`Error: ${error}`);
    return `Failed to get ${url}, message: ${error}`;
  }
};
