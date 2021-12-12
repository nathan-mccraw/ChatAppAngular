import { useState } from "react";
import { useAuthContext } from "./useAuthContext";
import { axios } from "axios";

export const useLogOut = () => {
  const [error, setError] = useState(null);
  const [isPending, setIsPending] = useState(null);
  const { dispatch } = useAuthContext();

  const logOut = async (userSession) => {
    setIsPending(true);
    setError(null);

    try {
      await axios.put("/api/signout", userSession);
      dispatch({ type: "LOGOUT" });
      setIsPending(false);
      setError(null);
    } catch (error) {
      console.log(error.message);
      setError(error);
      setIsPending(false);
    }
  };

  return { logOut, error, isPending };
};
