import "./Dashboard.scss";
import { useState } from "react";
import axios from "axios";

const Dashboard = () => {
  const [dashboardLoaded, setDashboardLoaded] = useState(false);

  return dashboardLoaded ? (
    <div className="dashboard">
      <h3>Hello there!</h3>
    </div>
  ) : (
    <div className="dashboard skeleton">
      <h3>Hello there!</h3>
    </div>
  );
};

export default Dashboard;
