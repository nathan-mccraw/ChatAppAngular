import React from "react";
import { useState } from "react";
import { Route, Routes, useNavigate } from "react-router-dom";
import "./App.css";
import SignInPage from "./components/SignIn";
import RegisterNewUser from "./components/RegisterNewUser";
import ModifyUserProfile from "./components/ModifyUserProfile";
import ChatPage from "./components/ChatPage";
import Modal from "react-bootstrap/Modal";
import SignOutModal from "./components/SignOutModal";
import axios from "axios";

const App = () => {
  const [userSession, setUserSession] = useState({
    id: "",
    userToken: "",
  });

  const [userProfile, setUserProfile] = useState({
    id: "",
    username: "",
    firstName: "",
    lastName: "",
    lastActive: "",
    DateCreated: "",
  });

  const [modalStates, setModalStates] = useState({
    isSignOutOpen: false,
    showSignOutModal: function () {
      setModalStates({ ...modalStates, isSignOutOpen: true });
    },
    hideSignOutModal: function () {
      setModalStates({ ...modalStates, isSignOutOpen: false });
    },
  });

  const navigate = useNavigate();

  const guestSignUp = () => {
    axios
      .get("/api/signup")
      .then((response) => {
        setUserSession(response.data);
        axios.get(`/api/users/${response.data.id}`).then((userResponse) => {
          setUserProfile(userResponse.data);
        });
        navigate("/Chat");
      })
      .catch((error) => {
        console.log(error.response.data);
      });
  };

  const signOut = () => {
    setUserProfile({
      id: "",
      username: "",
      firstName: "",
      lastName: "",
      lastActive: "",
      DateCreated: "",
    });
    axios
      .put("/api/SignOut", userSession)
      .then((_) => {
        setUserSession({
          id: "",
          userToken: "",
        });
      })
      .catch((error) => {
        console.log(error.response.data);
      });
    modalStates.hideSignOutModal();
    navigate("/");
  };

  return (
    <div>
      <Routes>
        <Route
          exact
          path="/"
          element={
            <SignInPage
              setUserSession={setUserSession}
              setUserProfile={setUserProfile}
              guestSignUp={guestSignUp}
            />
          }
        />
        <Route
          exact
          path="/Register"
          element={
            <RegisterNewUser
              setUserSession={setUserSession}
              setUserProfile={setUserProfile}
              guestSignUp={guestSignUp}
            />
          }
        />
        <Route
          exact
          path="/AccountSettings"
          element={
            <ModifyUserProfile
              userProfile={userProfile}
              setUserProfile={setUserProfile}
              userSession={userSession}
              setUserSession={setUserSession}
              signOut={signOut}
            />
          }
        />
        <Route
          exact
          path="/Chat"
          element={
            <ChatPage
              userProfile={userProfile}
              userSession={userSession}
              setUserSession={setUserSession}
              modalStates={modalStates}
              signOut={signOut}
            />
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
            signOut={signOut}
            hideSignOutModal={modalStates.hideSignOutModal}
          />
        </Modal.Body>
      </Modal>
    </div>
  );
};

export default App;
