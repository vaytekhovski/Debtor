import "./Dashboard.scss";
import { useEffect } from "react";
import { useAppDispatch, useAppSelector } from "../../../../hooks/redux-hooks";
import { fetchDashboard } from "../../../../store/dashboard.actions";

const Dashboard = () => {
  const User = useAppSelector((state) => state.user);
  const Dashboard = useAppSelector((state) => state.dashboard);

  const isDashboardLoaded = (): boolean => {
    return Dashboard.incomingAmount !== "";
  };

  return isDashboardLoaded() ? (
    <div className="dashboard">
      <div>
        <h3>{User.name}</h3>
      </div>
      <div className="status-card incoming">
        <h4>incoming: ${Dashboard.incomingAmount}</h4>
      </div>
      <div className="status-card outcoming">
        <h4>outcoming: ${Dashboard.outcomingAmount}</h4>
      </div>
    </div>
  ) : (
    <div className="dashboard skeleton">
      <div className="text-center">
        <h3>Loading user data...</h3>
      </div>
    </div>
  );
};

export default Dashboard;
