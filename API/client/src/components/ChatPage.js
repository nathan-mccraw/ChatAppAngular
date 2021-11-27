import React from "react";
import axios from "axios";
import { useEffect, useState, useRef } from "react";
import * as signalR from "@microsoft/signalr";
import FriendsSideBar from "./ChatPage/FriendsSideBar";
import ChatInput from "./ChatPage/ChatInput.js";
import ChatWindow from "./ChatPage/ChatWindow";
import Footer from "./ChatPage/Footer";
import TitleBar from "./ChatPage/TitleBar";
import { useNavigate } from "react-router";

const ChatPage = ({
  userProfile,
  userSession,
  setUserSession,
  modalStates,
  signOut,
}) => {
  const [connection, setConnection] = useState(null);
  const [messageArray, setMessageArray] = useState("");
  const [chatMessage, setChatMessage] = useState("");

  const latestChat = useRef(null);
  latestChat.current = messageArray;
  const navigate = useNavigate();

  useEffect(() => {
    const newConnection = new signalR.HubConnectionBuilder()
      .withUrl("/chathub")
      .withAutomaticReconnect()
      .build();
    setConnection(newConnection);

    axios.get("/api/messages").then((response) => {
      setMessageArray(response.data);
    });

    setTimeout(updateScroll, 1000);
  }, []);

  useEffect(() => {
    if (connection) {
      connection
        .start()
        .then((result) => {
          console.log("Good Connection");
          connection.on("ReceiveMessage", (message) => {
            console.log("receiving message");
            const updatedChat = [...latestChat.current];
            updatedChat.push(message);
            setMessageArray(updatedChat);
            setTimeout(updateScroll, 500);
          });
        })
        .catch((e) => console.log(e));
    }
  }, [connection]);

  const submitMessage = async (e) => {
    e.preventDefault();
    const message = {
      text: chatMessage,
      user: userSession,
    };

    if (connection.connectionStarted) {
      try {
        axios.post("/api/messages", message).catch((error) => {
          alert(error.response.data);
          console.log(error.response);
          if (error.response.status === 401) {
            signOut();
            navigate("/");
          }
        });
      } catch (error) {
        console.log(error);
      }
    } else {
      console.log("No connection to server");
    }
    setChatMessage("");
  };

  const updateScroll = () => {
    const chatRoomElement = document.getElementById("chatRoom");
    chatRoomElement.scrollTop = chatRoomElement.scrollHeight;
  };

  return (
    <div className="container-fluid">
      <div className="row">
        <FriendsSideBar />
        <div id="chatwindowcontainer" className="col p-0">
          <div id="chatwindow" className="col m-0">
            <TitleBar />
            <ChatWindow messageArray={messageArray} />
          </div>
          <div className="row m-0">
            <ChatInput
              submitMessage={submitMessage}
              chatMessage={chatMessage}
              setChatMessage={setChatMessage}
            />
            <Footer userProfile={userProfile} modalStates={modalStates} />
          </div>
        </div>
      </div>
    </div>
  );
};

export default ChatPage;
