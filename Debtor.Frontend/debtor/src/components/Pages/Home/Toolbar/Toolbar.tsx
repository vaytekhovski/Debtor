import { useEffect, useState } from "react";
import { Link } from "react-router-dom";
import "./Toolbar.scss";

const Toolbar = () => {
  const [isToolbarOpen, setIsToolbarOpen] = useState(false);

  return (
    <nav className="menu">
      <input
        defaultChecked={false}
        className="menu-toggler"
        id="menu-toggler"
        type="checkbox"
      ></input>
      <label htmlFor="menu-toggler"></label>
      <ul>
        <li className="menu-item">
          <Link to="/">📊</Link>
        </li>
        <li className="menu-item">
          <Link to="/transaction/create">💸</Link>
        </li>
        <li className="menu-item">
          <Link to="/settings">⚙️</Link>
        </li>
      </ul>
    </nav>
  );
};

export default Toolbar;
