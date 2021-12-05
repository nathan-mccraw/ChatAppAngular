import axios from "axios";
import React from "react";
import { useState } from "react";
import { Link } from "react-router-dom";
import Modal from "react-bootstrap/Modal";
import DeleteUserModal from "./DeleteUsereModal";

const ModifyUserProfile = ({
  userProfile,
  setUserProfile,
  userSession,
  setUserSession,
  signOut,
}) => {
  const [modalStates, setModalStates] = useState({
    isDeleteUserOpen: false,
    showDeleteUserModal: function () {
      setModalStates({ ...modalStates, isDeleteUserOpen: true });
    },
    hideDeleteUserModal: function () {
      setModalStates({ ...modalStates, isDeleteUserOpen: false });
    },
  });

  const [modifyUserFormState, setModifyUserFormState] = useState({
    user: userSession,
    username: "",
    password: "",
    confirmPassword: "",
    firstName: "",
    lastName: "",
  });

  const modifyUserFormChange = (e) => {
    setModifyUserFormState({
      ...modifyUserFormState,
      [e.target.name]: e.target.value,
    });
  };

  const submitUserChanges = (e) => {
    e.preventDefault();
    axios
      .put("/api/signin", modifyUserFormState)
      .then((response) => {
        setUserSession(response.data);
        axios.get(`/api/users/${response.data.userId}`).then((userResponse) => {
          setUserProfile(userResponse.data);
          setModifyUserFormState({
            user: userSession,
            username: "",
            password: "",
            confirmPassword: "",
            firstName: "",
            lastName: "",
          });
        });
        alert("User profile updated!");
      })
      .catch((error) => {
        alert(error.response.data);
      });
  };

  return (
    <div
      id="ModifyUserProfile"
      className="d-flex vh-100 justify-content-center align-items-center"
    >
      <form
        id="ModifyUserFormBox"
        className="col-auto bg-secondary border border-dark border-1 p-4"
        action="submit"
        onSubmit={submitUserChanges}
      >
        <div className="row">
          <div className="input-group mb-3">
            <span className="input-group-text">User Name:</span>
            <input
              type="text"
              value={modifyUserFormState.username}
              onChange={modifyUserFormChange}
              name="username"
              className="form-control"
              placeholder={userProfile.username}
            />
          </div>
        </div>
        <div className="row">
          <div className="input-group mb-3">
            <span className="input-group-text">Password:</span>
            <input
              type="password"
              value={modifyUserFormState.password}
              onChange={modifyUserFormChange}
              name="password"
              className="form-control"
              placeholder="Minimum 4 digits"
              required
            />
          </div>
        </div>
        <div className="row">
          <div className="input-group mb-3">
            <span className="input-group-text">Confirm Password:</span>
            <input
              type="password"
              value={modifyUserFormState.confirmPassword}
              onChange={modifyUserFormChange}
              name="confirmPassword"
              className="form-control"
              placeholder="Minimum 4 digits"
              required
            />
          </div>
        </div>
        <div className="row">
          <div className="input-group mb-3">
            <span className="input-group-text">First Name:</span>
            <input
              type="text"
              value={modifyUserFormState.firstName}
              onChange={modifyUserFormChange}
              name="firstName"
              className="form-control"
              placeholder={userProfile.firstName}
            />
          </div>
        </div>
        <div className="row">
          <div className="input-group mb-3">
            <span className="input-group-text">Last Name:</span>
            <input
              type="text"
              value={modifyUserFormState.lastName}
              onChange={modifyUserFormChange}
              name="lastName"
              className="form-control"
              placeholder={userProfile.lastName}
            />
          </div>
        </div>
        <div className="row justify-content-center">
          <div className="col-auto mb-3">
            <button type="submit" className="btn btn-success">
              Submit Changes
            </button>
          </div>
        </div>
        <div className="row justify-content-center">
          <div className="col-auto mb-3">
            <Link to="/Chat" className="btn btn-outline-warning border-0">
              Exit - Back to The Outpost
            </Link>
          </div>
        </div>
        <div className="row justify-content-center h6 text-primary">
          <button
            type="button"
            className="btn btn-outline-danger border-0"
            onClick={modalStates.showDeleteUserModal}
          >
            Delete Account
          </button>
        </div>
      </form>
      <Modal
        show={modalStates.isDeleteUserOpen}
        onHide={modalStates.hideDeleteUserModal}
        dialogClassName={"DeleteUserModal"}
      >
        <Modal.Header className="justify-content-center">
          <h3>Delete Profile?</h3>
        </Modal.Header>
        <Modal.Body>
          <DeleteUserModal
            signOut={signOut}
            hideDeleteUserModal={modalStates.hideDeleteUserModal}
          />
        </Modal.Body>
      </Modal>
    </div>
  );
};

export default ModifyUserProfile;
