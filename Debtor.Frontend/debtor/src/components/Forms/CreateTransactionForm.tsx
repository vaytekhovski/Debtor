import React, { useEffect, useState } from "react";
import {
  createStyles,
  makeStyles,
  Paper,
  Button,
  Switch,
} from "@material-ui/core";
import IconButton from "@mui/material/IconButton";
import PersonAddIcon from "@mui/icons-material/PersonAdd";

import CustomTextField from "./CustomTextField";
import CustomDropDown from "./CustomDropDown";
import { postUser } from "../../store/user-actions";
import { useAppDispatch, useAppSelector } from "../../hooks/redux-hooks";
import { postTransactions } from "../../store/transaction-actions";
import { useNavigate } from "react-router-dom";

const useStyles = makeStyles(() =>
  createStyles({
    form: {
      display: "flex",
      flexDirection: "column",
    },
    container: {
      backgroundColor: "#ffffff",
      padding: 30,
      textAlign: "center",
    },
    title: {
      margin: "0px 0 20px 0",
    },
    button: {
      margin: "20px 0",
    },
    row: {
      display: "flex",
      flexDirection: "row",
      flexWrap: "nowrap",
      alignItems: "center",
      justifyContent: "space-evenly",
    },
  })
);

type CreateTransactionValue = {
  userFromId: string;
  userToId: string;
  jointId?: string;
  amount: number;
  description: string;
  fromMe: boolean;
  selectedUser: string;
};

const CreateTransactionForm = () => {
  const classes = useStyles();
  const [values, setValues] = useState<CreateTransactionValue>({
    userFromId: "",
    userToId: "",
    jointId: "",
    amount: 0,
    description: "",
    fromMe: false,
    selectedUser: "",
  });

  const [isOpenAddPerson, setIsOpenAddPerson] = useState(false);
  const [addPerson, setAddPerson] = useState("");
  const dispatch = useAppDispatch();
  const User = useAppSelector((state) => state.user);
  const navigate = useNavigate();

  const handleChangeAddPerson = (
    event: React.ChangeEvent<HTMLInputElement>
  ) => {
    setAddPerson(event.target.value);
  };

  const handleSaveNewPerson = () => {
    dispatch(postUser({ name: addPerson, creatorId: User.id }));
    setIsOpenAddPerson(false);
    setAddPerson("");
  };

  const handleChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    setValues({
      ...values,
      [event.target.name]: event.target.value,
    });
    console.log(event.target.value);
  };

  const handleChangeSwitch = (event: React.ChangeEvent<HTMLInputElement>) => {
    setValues({
      ...values,
      [event.target.name]: event.target.checked,
    });
  };

  const handleSubmit = (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();

    const newTransaction = {
      userFromId: values.fromMe ? User.id! : values.selectedUser,
      userToId: !values.fromMe ? User.id! : values.selectedUser,
      jointId: values.jointId,
      amount: values.amount,
      description: values.description,
      id: "",
    };

    dispatch(postTransactions(newTransaction));
    navigate("/");
  };

  const getRecepientLabel = (): string => {
    return values.fromMe ? "Кому" : "От кого";
  };

  type ValueLabel = {
    value: string;
    label: string;
  };

  const friendsList = (): ValueLabel[] => {
    if (User.friends !== undefined && User.friends.length > 0) {
      return User.friends.map((friend) => {
        return {
          value: friend.id!,
          label: friend.name!,
        };
      })!;
    }
    return [];
  };

  return (
    <Paper className={classes.container}>
      <h2 className={classes.title}>Create Transaction Form</h2>
      <form onSubmit={(e) => handleSubmit(e)} className={classes.form}>
        <div className={classes.row}>
          <h5>Я получаю</h5>
          <Switch
            onChange={handleChangeSwitch}
            inputProps={{ "aria-label": "controlled" }}
            name={"fromMe"}
            value={values.fromMe}
          />
          <h5>Я отдаю</h5>
        </div>
        <div className={classes.row}>
          <CustomDropDown
            label={getRecepientLabel()}
            name={"selectedUser"}
            changeHandler={handleChange}
            values={friendsList()}
            currentValue={values.selectedUser}
            style={{ flexGrow: 1 }}
          />
          <IconButton
            aria-label="person-add"
            onClick={() => setIsOpenAddPerson((open) => !open)}
          >
            <PersonAddIcon />
          </IconButton>
        </div>
        {isOpenAddPerson && (
          <>
            <CustomTextField
              changeHandler={handleChangeAddPerson}
              label={"Новый пользователь"}
              name={"addPerson"}
            />
            <Button
              variant={"contained"}
              className={classes.button}
              onClick={() => handleSaveNewPerson()}
            >
              Создать
            </Button>
          </>
        )}
        <CustomTextField
          changeHandler={handleChange}
          label={"Сумма ($)"}
          name={"amount"}
        />
        <CustomTextField
          changeHandler={handleChange}
          label={"Описание"}
          name={"description"}
        />
        <Button
          type={"submit"}
          variant={"contained"}
          className={classes.button}
        >
          Создать
        </Button>
      </form>
    </Paper>
  );
};

export default CreateTransactionForm;
