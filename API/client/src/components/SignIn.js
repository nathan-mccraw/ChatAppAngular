import React, { useState } from "react";
import { Link, useNavigate } from "react-router-dom";
import "bootstrap-icons/font/bootstrap-icons.css";
import { useAuthContext } from "../Hooks/useAuthContext";
import { useLogIn } from "./../Hooks/useLogIn";

const SignInPage = ({ guestSignUp }) => {
  const [signInFormState, setSignInFormState] = useState({
    username: "",
    password: "",
  });

  const { isAuthed } = useAuthContext();
  const { logIn } = useLogIn();

  let navigate = useNavigate();

  const signOn = async (e) => {
    e.preventDefault();
    await logIn(signInFormState);
    if (!isAuthed) {
      setSignInFormState({ ...signInFormState, password: "" });
    } else {
      navigate("/Chat");
    }
  };

  const formChange = (e) => {
    setSignInFormState({
      ...signInFormState,
      [e.target.name]: e.target.value,
    });
  };

  return (
    <div className="row m-0 vh-100" id="signIn">
      <div className="col-auto d-flex justify-content-center">
        <form
          id="signInForm"
          className="align-self-end"
          action="submit"
          onSubmit={signOn}
        >
          <div className="row">
            <div className="input-group mb-3">
              <span className="input-group-text">
                <i className="bi bi-person-circle" style={{ fontsize: 15 }}></i>
              </span>
              <input
                type="text"
                value={signInFormState.username}
                onChange={formChange}
                name="username"
                className="form-control"
                placeholder="Username"
                required
              />
            </div>
          </div>
          <div className="row">
            <div className="input-group mb-3">
              <span className="input-group-text">Password:</span>
              <input
                type="password"
                value={signInFormState.password}
                onChange={formChange}
                name="password"
                className="form-control"
                placeholder="Minimum 4 digits"
                required
              />
            </div>
          </div>
          <div className="row justify-content-center">
            <div className="col-auto mb-4">
              <button type="submit" className="btn btn-success">
                Sign In
              </button>
            </div>
          </div>
          <div className="row justify-content-center h6">
            <Link
              to="/Register"
              className="col-auto btn btn-outline-light border-0 mb-1"
            >
              Register As New User
            </Link>
          </div>
          <div className="row justify-content-center mb-4 h6 text-primary">
            <button
              type="button"
              className="col-auto btn btn-outline-warning border-0"
              onClick={() => guestSignUp()}
            >
              Sign In As Guest
            </button>
          </div>
        </form>
      </div>
    </div>
  );
};
export default SignInPage;
