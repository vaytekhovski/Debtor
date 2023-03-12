import "./App.scss";
import { useAuth0 } from "@auth0/auth0-react";
import Home from "./components/Pages/Home/Home";
import Authorization from "./components/Pages/Authorization/Authorization";
import SettingsPage from "./components/Pages/Settings/Settings";
import CallbackPage from "./components/Pages/Authorization/Callback";
import { BrowserRouter, Routes, Route, Navigate } from "react-router-dom";
import CreateTransactionPage from "./components/Pages/CreateTransactions/CreateTransaction";
import Toolbar from "./components/Pages/Home/Toolbar/Toolbar";
import TransactionComponent from "./components/components/TransactionComponent";
import Layout from "./components/Layout";

function App() {
  const { isLoading, error } = useAuth0();

  return (
    <div className="application">
      <div className="content">
        {error && <p>Authentication Error: {error.message}</p>}
        {!error && isLoading && <p>Loading...</p>}
        {!error && !isLoading && (
          <>
            <Routes>
              <Route element={<Layout />}>
                <Route path="/" element={<Home />} />
                <Route
                  path="/transaction/:id"
                  element={<TransactionComponent />}
                />
                <Route
                  path="transaction/create"
                  element={<CreateTransactionPage />}
                />
                <Route path="auth" element={<Authorization />} />
                <Route path="auth/callback" element={<CallbackPage />} />
                <Route path="settings" element={<SettingsPage />} />
              </Route>
              <Route path="*" element={<Navigate to="/" />} />
            </Routes>
          </>
        )}
      </div>
    </div>
  );
}

export default App;
