import axios from "axios";
import React from "react";
import { useState } from "react";
import { Link } from 'react-router-dom';

const RegisterNewUser = () => {
    const [registerFormState, setRegisterFormState] = useState({ username: "", password: "", confirmPassword: "", firstName: "", lastName: "" });

    const registerFormChange = (e) => {
        setRegisterFormState({ ...registerFormState, [e.target.name]: e.target.value });
    }

    const registerNewUser = async (e) => {
        e.preventDefault();
        let res = await axios.post("/api/signup", registerFormState);
        console.log("Registered New User:", res);
        console.log("Redirect to ChatRoom");
    };

    return (
        <div id="RegisterNewUser" className="container-fluid vh-100 d-flex justify-content-center align-items-center">
            <div className="col-auto">
                <form action="submit" onSubmit={registerNewUser}>
                    <div id="RegisterFormBox" className="bg-secondary border border-dark border-1 p-4">
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
                    <div className="row justify-content-center">
                        <div className="col-auto mt-3">
                            <button type="submit" className="btn btn-primary">
                                Register
                            </button>
                        </div>
                    </div>
                    </div>
                    <div className="row justify-content-center mt-2 h6 text-primary">
                        <button type="button" className="col-auto btn btn-warning border-0" onClick={() => console.log("Sign In As Guest")}>
                            Sign In As Guest
                        </button>
                    </div>
                    <div className="row justify-content-center h6 text-primary">
                        <Link to="/" className="col-auto btn btn-dark border-0">
                            Sign In As Existing User
                        </Link>
                    </div>
                </form>
            </div>
        </div>
    )
}

export default RegisterNewUser;