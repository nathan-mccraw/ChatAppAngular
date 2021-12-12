// import { axios } from "axios";
// import React, { createContext, useContext, useState } from "react";

// const authConext = createContext();
// const API_URL = process.env.BaseAddress;

// export function useAuth() {
//   const [authed, setAuthed] = useState(false);
//   const [userProfile, setUserProfile] = useState({
//     id: "",
//     username: "",
//     firstName: "",
//     lastName: "",
//     lastActive: "",
//     DateCreated: "",
//   });

//   const [userSession, setUserSession] = useState({
//     id: "",
//     userId: "",
//     userToken: "",
//     hasOtherActiveSessions: false,
//   });

//   const clearUser = () => {
//     setUserProfile({
//       id: "",
//       username: "",
//       firstName: "",
//       lastName: "",
//       lastActive: "",
//       DateCreated: "",
//     });

//     setUserSession({
//       id: "",
//       userId: "",
//       userToken: "",
//       hasOtherActiveSessions: false,
//     });
//   };

//   function validateUser() {
//     axios
//       .post(API_URL + "validateUser", userSession)
//       .then((response) => {
//         if (response.status === 200) {
//           setAuthed(true);
//         } else {
//           setAuthed(false);
//         }
//       })
//       .catch((error) => {
//         setAuthed(false);
//         console.log(error);
//       });
//   }

//   function registerNewUser(registerFormState) {
//     axios
//       .post("/api/signup", registerFormState)
//       .then((response) => {
//         setUserSession(response.data);
//         axios.get(`/api/users/${response.data.userId}`).then((userResponse) => {
//           setUserProfile(userResponse.data);
//         });
//       })
//       .catch((error) => {
//         alert(error.response.data);
//       });
//   }

//   function registerNewGuest() {
//     axios
//       .get("/api/signup")
//       .then((response) => {
//         setUserSession(response.data);
//         axios.get(`/api/users/${response.data.userId}`).then((userResponse) => {
//           setUserProfile(userResponse.data);
//         });
//       })
//       .catch((error) => {
//         console.log(error.response);
//       });
//   }

//   function logIn(signInModel) {
//     axios
//       .post(API_URL + "signin", signInModel)
//       .then((response) => {
//         if (response.status === 200) {
//           setUserSession(response.data);
//           setAuthed(true);
//           return userSession.userId;
//         } else {
//           setAuthed(false);
//           clearUser();
//         }
//       })
//       .then((userId) => {
//         axios.get(`${API_URL}users/${userId}`).then((response) => {
//           setUserProfile(response.data);
//         });
//       })
//       .catch((error) => {
//         setAuthed(false);
//         clearUser();
//         console.log(error);
//       });
//   }

//   function modifyUserProfile(newProfileData) {
//     axios
//       .put("/api/signin", newProfileData)
//       .then((response) => {
//         setUserSession(response.data);
//         axios.get(`/api/users/${response.data.userId}`).then((userResponse) => {
//           setUserProfile(userResponse.data);
//         });
//         alert("User profile updated!");
//       })
//       .catch((error) => {
//         alert(error.response.data);
//       });
//   }

//   function logOut() {
//     axios.put(API_URL + "signout", userSession).then(() => {
//       setAuthed(false);
//       clearUser();
//     });
//   }

//   return {
//     authed,
//     userProfile,
//     userSession,
//     validateUser,
//     registerNewUser,
//     registerNewGuest,
//     modifyUserProfile,
//     logIn,
//     logOut,
//   };
// }

// export function AuthProvider({ children }) {
//   const auth = useAuth();

//   return <authConext.Provider value={auth}>{children}</authConext.Provider>;
// }

// export default function AuthConsumer() {
//   return useContext(authConext);
// }
