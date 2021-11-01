import axios from "axios";
import React from "react";
import { useState } from "react";

const RegisterModal = ({ showSignInModal }) => {
    const [registerFormState, setRegisterFormState] = useState({ username: "", password: "", confirmPassword: "", firstName: "", lastName: "" });

    const registerFormChange = (e) => {
        setRegisterFormState({ ...registerFormState, [e.target.name]: e.target.value });
    }

    const registerNewUser = async (e) => {
        e.preventDefault();
        let res = await axios.post("/api/signup", registerFormState);
        console.log(res);
    };

    return (
        <div>
            <form action="submit" onSubmit={registerNewUser}>
                <div className="row">
                    <div className="input-group mb-3">
                        <span className="input-group-text">User Name:</span>
                        <input
                            type="text"
                            value={registerFormState.username}
                            onChange={registerFormChange}
                            name="username"
                            className="form-control"
                            placeholder="20 Character Maximum"
                            aria-label="Sizing example input"
                            aria-describedby="inputGroup-sizing-default"
                            required
                        />
                    </div>
                </div>
                <div className="row">
                    <div className="input-group mb-3">
                        <span className="input-group-text">Password:</span>
                        <input
                            type="password"
                            value={registerFormState.password}
                            onChange={registerFormChange}
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
                            value={registerFormState.confirmPassword}
                            onChange={registerFormChange}
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
                            value={registerFormState.firstName}
                            onChange={registerFormChange}
                            name="firstName"
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
                        <span className="input-group-text">Last Name:</span>
                        <input
                            type="text"
                            value={registerFormState.lastName}
                            onChange={registerFormChange}
                            name="lastName"
                            className="form-control"
                            placeholder="Minimum 4 digits"
                            aria-label="Sizing example input"
                            aria-describedby="inputGroup-sizing-default"
                            required
                        />
                    </div>
                </div>
                <div className="row justify-content-center">
                    <div className="col-auto mb-4">
                        <button type="submit" className="btn btn-success">
                            Register
                        </button>
                    </div>
                </div>
                <div className="row justify-content-center mb-0 pb-0 mt-4 pt-4 h6 text-primary">
                    <button type="button" className="btn btn-outline-primary border-0" onClick={showSignInModal}>
                        Sign In As Existing User
                    </button>
                </div>
            </form>
        </div>
    )
}

export default RegisterModal;