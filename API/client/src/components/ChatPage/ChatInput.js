import React from "react";

const ChatInput = ({
    submitMessage,
    chatMessage,
    setChatMessage
}) => {

    return (
        <div className="col">
            <form action="submit" onSubmit={submitMessage}>
                <div className="row m-3 mt-4 mb-4 justify-content-center align-items-center">
                    <div className="col-9">
                        <textarea
                            className="form-control"
                            rows="3"
                            placeholder="Chat..."
                            value={chatMessage}
                            onChange={(e) => setChatMessage(e.target.value)}
                            required
                        ></textarea>
                    </div>
                    <div className="col-auto">
                        <button type="submit" className="btn btn-outline-secondary">
                            Send
                        </button>
                    </div>
                </div>
            </form>
        </div>
    );
};

export default ChatInput;