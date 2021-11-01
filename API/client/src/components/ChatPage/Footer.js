import React from "react";
import { useState } from "react";
import { Link } from 'react-router-dom';
import Modal from "react-bootstrap/Modal";
import SignOutModal from "../SignOutModal";

const Footer = ({ user, setUser }) => {
    const [modalStates, setModalStates] = useState({
        isSignOutOpen: false,
        showSignOutModal: function () { setModalStates({ ...modalStates, isSignOutOpen: true }) },
        hideSignOutModal: function () { setModalStates({ ...modalStates, isSignOutOpen: false }) },
    });

    const signOut = () => {
        setUser({
            userId: "1",
            username: "Guest",
            firstName: "Guest",
            lastName: "Guest",
            DateCreated: "2021-09-24T00:28:21",
        })
    };

    return (
        <div className="row mt4 justify-content-center align-items-baseline">
            <div className="col-auto p-0">sending message as:</div>
            <div className="col-auto p-0 h5 text-primary">
                <Link to="AccountSettings" className="btn btn-sm btn-outline-primary border-0 m-2">
                    <i className="me-1 bi bi-person-lines-fill"></i>
                    {user.username}
                </Link>
            </div>
            {user.username === "Guest" &&
                <div className="col-auto p-1">
                <Link to="/" className="btn btn-sm btn-outline-primary rounded-pill m-2" >
                        Sign-in
                    </Link>
                </div>}
            {user.username !== "Guest" &&
                <div className="col-auto p-1">
                    <button
                        type="button"
                        className="btn btn-sm btn-outline-primary rounded-pill m-2"
                        onClick={modalStates.showSignOutModal}
                    >
                        Sign Out
                    </button>
                </div>}
            <Modal
                show={modalStates.isSignOutOpen}
                onHide={modalStates.hideSignOutModal}
                dialogClassName={"SignOutModal"} >
                <Modal.Header className="justify-content-center">
                    <h3>Sign Out?</h3>
                </Modal.Header>
                <Modal.Body>
                    <SignOutModal signOut={signOut} hideSignOutModal={modalStates.hideSignOutModal} />
                </Modal.Body>
            </Modal>
        </div>
    )
}

export default Footer;