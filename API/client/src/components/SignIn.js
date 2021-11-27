import React from "react";
import axios from "axios";
import { useState } from "react";
import { Link, useNavigate } from "react-router-dom";
import "bootstrap-icons/font/bootstrap-icons.css";
import { Container, Row } from "react-bootstrap";

const SignInPage = ({ setUserSession, setUserProfile, guestSignUp }) => {
  const [signInFormState, setSignInFormState] = useState({
    username: "",
    password: "",
  });

  let navigate = useNavigate();

  const signIn = (e) => {
    e.preventDefault();
    axios
      .post("/api/signin", signInFormState)
      .then((response) => {
        setUserSession(response.data);
        axios.get(`/api/users/${response.data.id}`).then((userResponse) => {
          setUserProfile(userResponse.data);
        });
        navigate("/Chat");
      })
      .catch((error) => {
        alert(error.response.data);
        setSignInFormState({ ...signInFormState, password: "" });
      });
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
          onSubmit={signIn}
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
