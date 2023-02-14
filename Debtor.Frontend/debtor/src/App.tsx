import "./App.scss";
import { useAuth0 } from "@auth0/auth0-react";
import Home from "./components/Pages/Home/Home";
import Authorization from "./components/Pages/Authorization/Authorization";
import SettingsPage from "./components/Pages/Settings/Settings";
import CallbackPage from "./components/Pages/Authorization/Callback";
import { BrowserRouter, Routes, Route } from "react-router-dom";

function App() {
  const { isLoading, error } = useAuth0();

  return (
    <div className="application">
      <div className="content">
        {error && <p>Authentication Error: {error.message}</p>}
        {!error && isLoading && <p>Loading...</p>}
        {!error && !isLoading && (
          <BrowserRouter>
            <Routes>
              <Route path="*" element={<Home />} />
              <Route path="/" element={<Home />} />
              <Route path="auth" element={<Authorization />} />
              <Route path="auth/callback" element={<CallbackPage />} />
              <Route path="settings" element={<SettingsPage />} />
            </Routes>
          </BrowserRouter>
        )}
      </div>
    </div>
  );
}

export default App;
