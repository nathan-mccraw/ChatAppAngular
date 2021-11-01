import React from "react";
import { useState } from "react";
import { BrowserRouter as Router, Route, Switch } from 'react-router-dom';
import "./App.css";
import SignInPage from "./components/SignIn";
import RegisterNewUser from "./components/RegisterNewUser";
import ModifyUserProfile from "./components/ModifyUserProfile";
import ChatPage from "./components/ChatPage";

const App = () => {
    const [user, setUser] = useState({
        userId: "1",
        username: "Guest",
        firstName: "Guest",
        lastName: "Guest",
        DateCreated: "2021-09-24T00:28:21",
    });

    return (
        <Router>
            <Switch>
                <Route exact path="/">
                    <SignInPage setUser={setUser} />
                </Route>
                <Route exact path="/AccountSettings">
                    <ModifyUserProfile user={user} setUser={setUser} />
                </Route>
                <Route exact path="/Register">
                    <RegisterNewUser user={user} />
                </Route>
                <Route exact path="/Chat">
                    <ChatPage />
                </Route>
            </Switch>
        </Router>
    );
};

export default App;

//<div className="App container-fluid">
        //    <div className="row">
        //        <FriendsSideBar otherUsersArray={otherUsersArray} />
        //        <div id="chatWindowContainer" className="col p-0">
        //            <div id="chatWindow" className="col m-0">
        //                <TitleBar />
        //                <ChatWindow messageArray={messageArray} />
        //            </div>
        //            <div className="row m-0">
        //                <ChatInput
        //                    submitMessage={submitMessage}
        //                    chatMessage={chatMessage}
        //                    setChatMessage={setChatMessage}
        //                />
        //                <Footer
        //                    user={user}
        //                    setUser={setUser}
        //                />
        //            </div>
        //        </div>
        //    </div>
        //</div>