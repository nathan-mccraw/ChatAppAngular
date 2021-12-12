import React from "react";
import { useLocation, Navigate } from "react-router-dom";
import { useAuthContext } from "./../Hooks/useAuthContext";

export function RequireAuth({ children }) {
  const { isAuthed } = useAuthContext();
  const location = useLocation();

  return isAuthed === true ? (
    children
  ) : (
    <Navigate to="/" replace state={{ path: location.pathname }} />
  );
}
