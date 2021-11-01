import React from "react";
import Message from './Message.js'

const ChatWindow = ({ messageArray }) => {
    return (
        <div id="chatRoom" className="row overflow-auto flex-grow-1 m-4 p-4 pt-0 pb-0 border border-3 border-secondary rounded shadow">
            <div className="col">
                {messageArray &&
                    messageArray.map((message) => (
                        <Message key={message.messageId} message={message} messageArray={messageArray} />
                    ))}
            </div>
        </div>
    );
};

export default ChatWindow;