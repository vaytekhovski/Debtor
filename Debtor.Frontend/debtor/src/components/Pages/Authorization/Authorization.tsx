import React from "react";
import "./Authorization";

// eslint-disable-next-line @typescript-eslint/no-empty-interface
export interface IAuthorizationPageProps {}

const AuthorizationPage: React.FunctionComponent<IAuthorizationPageProps> = (
  props
) => {
  return (
    <div className="authorization">
      <h3>AUTHORIZATION PAGE</h3>
    </div>
  );
};

export default AuthorizationPage;
