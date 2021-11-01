import axios from "axios";
import React from "react";
import { useState } from "react";

const ModifyUserModal = ({ user, signOut }) => {
    const [modifyUserFormState, setModifyUserFormState] = useState({ userId: user.userId, username: "", password: "", confirmPassword: "", firstName: "", lastName: "" });

    const modifyUserFormChange = (e) => {
        setModifyUserFormState({ ...modifyUserFormState, [e.target.name]: e.target.value });
    }

    const submitUserChanges = async (e) => {
        e.preventDefault();
        let res = await axios.put("/api/signin", modifyUserFormState);
        console.log(res);
    };

    const deleteUser = async (e) => {
        e.preventDefault();
        await axios.delete("/api/signin", user);
        signOut();
    }

    return (
        <div>
            <form action="submit" onSubmit={submitUserChanges}>
                <div className="row">
                    <div className="input-group mb-3">
                        <span className="input-group-text">User Name:</span>
                        <input
                            type="text"
                            value={modifyUserFormState.username}
                            onChange={modifyUserFormChange}
                            name="username"
                            className="form-control"
                            placeholder={user.username}
                            aria-label="Sizing example input"
                            aria-describedby="inputGroup-sizing-default"
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
                            aria-label="Sizing example input"
                            aria-describedby="inputGroup-sizing-default"
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
                            aria-label="Sizing example input"
                            aria-describedby="inputGroup-sizing-default"
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
                            placeholder={user.firstName}
                            aria-label="Sizing example input"
                            aria-describedby="inputGroup-sizing-default"
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
                            placeholder={user.lastName}
                            aria-label="Sizing example input"
                            aria-describedby="inputGroup-sizing-default"
                        />
                    </div>
                </div>
                <div className="row justify-content-center">
                    <div className="col-auto mb-4">
                        <button type="submit" className="btn btn-success">
                            Submit Changes
                        </button>
                    </div>
                </div>
                <div className="row justify-content-center mb-0 pb-0 mt-4 pt-4 h6 text-primary">
                    <button type="button" className="btn btn-outline-danger border-0" onClick={deleteUser}>
                        Delete
                    </button>
                </div>
            </form>
        </div>
    )
}

export default ModifyUserModal;