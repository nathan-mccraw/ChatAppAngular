import React from "react";
import Friend from "./Friend";

const FriendsSideBar = ({ otherUsersArray }) => {
    return (
        <div className="col-md-auto p-0 pt-1 h-100 border-end border-secondary border-4">
            <div className="row justify-content-center m-0 p-0 ps-4 pe-4 mt-4 pb-2 border-bottom h5 header">
                <span className="col-auto">Users Online</span>
                <i className="col-auto bi bi-people-fill ms-1"></i>
            </div>
            <div id="friendsSideBar" className="overflow-auto pt-3">
                {(otherUsersArray) && otherUsersArray.map((otherUser) => (
                    <Friend key={otherUser.userId} otherUser={otherUser} />
                ))}
            </div>
        </div>
    );
};

export default FriendsSideBar;