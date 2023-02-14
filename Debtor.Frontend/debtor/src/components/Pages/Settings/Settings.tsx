import React from "react";
import "./Settings.scss";

// eslint-disable-next-line @typescript-eslint/no-empty-interface
export interface ISettingsPageProps {}

const SettingsPage: React.FunctionComponent<ISettingsPageProps> = (props) => {
  return (
    <div className="settings">
      <h3>Settings PAGE</h3>
    </div>
  );
};

export default SettingsPage;
