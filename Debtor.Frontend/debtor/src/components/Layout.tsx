import { IconButton } from "@material-ui/core";
import React, { useEffect } from "react";
import { useNavigate, Outlet } from "react-router-dom";
import "./Layout.scss";
import ArrowBackIosIcon from "@mui/icons-material/ArrowBackIos";
import Toolbar from "./Pages/Home/Toolbar/Toolbar";

const Layout = () => {
  const navigate = useNavigate();

  const handleGoBack = () => {
    navigate(-1);
  };
  return (
    <>
      <IconButton onClick={() => handleGoBack()}>
        <ArrowBackIosIcon />
      </IconButton>
      <Outlet />
      <Toolbar />
    </>
  );
};

export default Layout;
