import React from "react";
import { useLocation, Navigate } from "react-router-dom";
import { useAuthContext } from "./../Hooks/useAuthContext";

export function RequireNotAuthed({ children }) {
  const { isAuthed } = useAuthContext();
  const location = useLocation();

  return isAuthed === false ? (
    children
  ) : (
    <Navigate to="/chat" replace state={{ path: location.pathname }} />
  );
}
