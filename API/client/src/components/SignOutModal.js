import React from "react";

const SignOutModal = ({ signOut, hideSignOutModal }) => {
    

    return (
        <div>
            <div className="row justify-content-center">
                <div className="col-auto mb-4">
                    <button type="button" className="btn btn-success" onClick={signOut}>
                        Sign Out
                    </button>
                </div>
            </div>
            <div className="row justify-content-center mb-0 pb-0 mt-4 pt-4 h6">
                <button type="button" className="btn btn-outline-primary border-0" onClick={hideSignOutModal}>
                    Cancel
                </button>
            </div>
        </div>
    );
};
export default SignOutModal;