import React from "react";
import axios from "axios";
import { useState } from "react";
import { useNavigate } from "react-router-dom";

const DeleteUserModal = ({ signOut, hideDeleteUserModal }) => {
  const [signInFormState, setSignInFormState] = useState({
    username: "",
    password: "",
  });

  let navigate = useNavigate();

  const formChange = (e) => {
    setSignInFormState({
      ...signInFormState,
      [e.target.name]: e.target.value,
    });
  };

  const deleteUser = (e) => {
    e.preventDefault();
    axios
      .delete("/api/signin", { data: signInFormState })
      .then((_) => {
        signOut();
        navigate("/");
      })
      .catch((error) => {
        console.log(error.response.data);
      });
  };

  return (
    <div className="col-auto d-flex justify-content-center">
      <form
        id="signInForm"
        className="align-self-end"
        action="submit"
        onSubmit={deleteUser}
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
            <button type="submit" className="btn btn-danger">
              Delete User
            </button>
          </div>
        </div>
        <div className="row justify-content-center mb-4 h6 text-primary">
          <button
            type="button"
            className="col-auto btn btn-outline-primary border-0"
            onClick={hideDeleteUserModal}
          >
            Cancel
          </button>
        </div>
      </form>
    </div>
  );
};

export default DeleteUserModal;
