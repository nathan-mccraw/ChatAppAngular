import React from "react";
import { Link } from "react-router-dom";

const Footer = ({ userProfile, modalStates }) => {
  return (
    <div className="row mt4 justify-content-center align-items-baseline">
      <div className="col-auto p-0">sending message as:</div>
      {userProfile.firstName === "Guest" && (
        <div className="col-auto p-1">
          <div className="row align-items-center">
            <div className="col-auto ms-2 h5">{userProfile.username}</div>
            <Link
              to="/"
              className="col-auto btn btn-sm btn-outline-primary rounded-pill m-2"
            >
              Sign-in
            </Link>
          </div>
        </div>
      )}
      {userProfile.firstName !== "Guest" && (
        <div className="col-auto">
          <div className="row">
            <div className="col-auto p-0 h5 text-primary">
              <Link
                to="/AccountSettings"
                className="btn btn-sm btn-outline-primary border-0 m-2"
              >
                <i className="me-1 bi bi-person-lines-fill"></i>
                {userProfile.username}
              </Link>
            </div>
            <div className="col-auto p-1">
              <button
                type="button"
                className="btn btn-sm btn-outline-primary rounded-pill m-2"
                onClick={modalStates.showSignOutModal}
              >
                Sign Out
              </button>
            </div>
          </div>
        </div>
      )}
    </div>
  );
};

export default Footer;
