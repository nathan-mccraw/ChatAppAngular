import React from "react";
import { useAuth } from "../../../Authentication/useAuth";

const Message = ({ message, messageArray }) => {
  const { userProfile } = useAuth();

  let isNotSameDate = true;
  let index = messageArray.indexOf(message);
  let currentDate = new Date(message.dateCreated);
  let prevDate =
    index !== 0 ? new Date(messageArray[index - 1].dateCreated) : null;
  if (
    prevDate !== null &&
    currentDate.getDate() === prevDate.getDate() &&
    currentDate.getMonth() === prevDate.getMonth()
  ) {
    isNotSameDate = false;
  }
  prevDate = new Date(message.dateCreated);

  return (
    <div className="row align-items-baseline m-3">
      {isNotSameDate && (
        <div>
          <hr className="mb-0" style={{ color: "red", height: 4 }} />
          <div className="col-auto fw-lighter text-danger mb-3 text-center">
            {`${
              currentDate.getMonth() + 1
            }/${currentDate.getDate()}/${currentDate.getFullYear()}`}
          </div>
        </div>
      )}
      {message.userId !== userProfile.id ? (
        <div className="row">
          <div className="col-auto">{message.username}: </div>
          <div className="col-6 text-light bg-info me-4 ms-1 p-1 ps-2 pe-3 shadow border border-dark rounded">
            {message.text}
          </div>
          <div
            className="col-auto fw-lighter"
            style={{ letterSpacing: ".15rem" }}
          >
            {currentDate.toLocaleTimeString()}
          </div>
        </div>
      ) : (
        <div className="row justify-content-end">
          <div
            className="col-auto fw-lighter"
            style={{ letterSpacing: ".15rem" }}
          >
            {currentDate.toLocaleTimeString()}
          </div>
          <div className="col-6 text-light bg-success me-1 ms-4 p-1 ps-3 pe-2 shadow border border-dark rounded">
            {message.text}
          </div>
          <div className="col-auto">: {message.username}</div>
        </div>
      )}
    </div>
  );
};

export default Message;
