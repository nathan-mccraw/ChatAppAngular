import React, { useState } from "react";
import { Route, Routes } from "react-router-dom";

import "./App.css";

import SignInPage from "./components/SignIn";
import RegisterNewUser from "./components/RegisterNewUser";
import ModifyUserProfile from "./components/ModifyUserProfile";
import ChatPage from "./components/ChatPage/ChatPage";
import Modal from "react-bootstrap/Modal";
import SignOutModal from "./components/SignOutModal";

import { RequireAuth } from "./RouteHelpers/RequireAuth";
import { RequireNotAuthed } from "./RouteHelpers/RequireNotAuthed";
import { useLogOut } from "./Hooks/useLogOut";
import { useSignUpGuest } from "./Hooks/useSignUpGuest";

const App = () => {
  const [modalStates, setModalStates] = useState({
    isSignOutOpen: false,
    showSignOutModal: function () {
      setModalStates({ ...modalStates, isSignOutOpen: true });
    },
    hideSignOutModal: function () {
      setModalStates({ ...modalStates, isSignOutOpen: false });
    },
  });

  const {
    logOut,
    error: logOutError,
    isPending: logOutIsPending,
  } = useLogOut();
  const {
    signUpGuest,
    error: signUpGuestError,
    isPending: signUpGuestIsPending,
  } = useSignUpGuest();

  const guestSignUp = async () => {
    await signUpGuest();
    console.log("Ran guest signup function in app.js");
  };

  const logOff = async () => {
    await logOut();
    modalStates.hideSignOutModal();
    console.log("Ran logout function in app.js");
  };

  return (
    <div>
      <Routes>
        <Route
          exact
          path="/"
          element={
            <RequireNotAuthed>
              <SignInPage guestSignUp={guestSignUp} />
            </RequireNotAuthed>
          }
        />
        <Route
          exact
          path="/Register"
          element={
            <RequireNotAuthed>
              <RegisterNewUser guestSignUp={guestSignUp} />
            </RequireNotAuthed>
          }
        />
        <Route
          exact
          path="/AccountSettings"
          element={
            <RequireAuth>
              <ModifyUserProfile logOff={logOff} />
            </RequireAuth>
          }
        />
        <Route
          exact
          path="/Chat"
          element={
            <RequireAuth>
              <ChatPage modalStates={modalStates} logOff={logOff} />
            </RequireAuth>
          }
        />
      </Routes>
      <Modal
        show={modalStates.isSignOutOpen}
        onHide={modalStates.hideSignOutModal}
        dialogClassName={"SignOutModal"}
      >
        <Modal.Header className="justify-content-center">
          <h3>Sign Out?</h3>
        </Modal.Header>
        <Modal.Body>
          <SignOutModal
            logOff={logOff}
            hideSignOutModal={modalStates.hideSignOutModal}
          />
        </Modal.Body>
      </Modal>
    </div>
  );
};

export default App;
