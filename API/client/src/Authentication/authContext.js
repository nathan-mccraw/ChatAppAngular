import React from "react";
import axios from "axios";
import { createContext, useReducer, useEffect } from "react";

export const AuthContext = createContext();

export const authReducer = (state, action) => {
  switch (action.type) {
    case "VALIDATE_USER":
      return { ...state, isAuthed: true };
    case "INVALIDATE_USER":
      return { ...state, isAuthed: false };
    case "UPDATE_PROFILE":
      return { ...state, userProfile: action.payload };
    case "UPDATE_SESSION":
      return { ...state, userSession: action.payload };
    case "LOGOUT":
      return {
        ...state,
        isAuthed: false,
        userProfile: null,
        userSession: null,
      };
    default:
      return state;
  }
};

export const AuthContextProvider = ({ children }) => {
  const [state, dispatch] = useReducer(authReducer, {
    isAuthed: false,
    userProfile: null,
    userSession: null,
  });

  console.log(state);

  useEffect(() => {
    console.log(state.userSession);
    const refresh = async () => {
      try {
        let sessResponse = await axios.post("/api/ValidateUser", {
          ...state.userSession,
        });
        if (sessResponse.status === 200) {
          dispatch({ type: "UPDATE_SESSION", payload: sessResponse.data });
          let userId = sessResponse.data.userId;
          let userResponse = await axios.get(`/api/users/${userId}`);
          dispatch({ type: "UPDATE_PROFILE", payload: userResponse.data });
        }
      } catch (error) {
        dispatch({ type: "LOGOUT" });
      }
    };

    refresh();
  }, []);

  return (
    <AuthContext.Provider value={{ ...state, dispatch }}>
      {children}
    </AuthContext.Provider>
  );
};
