import { useState } from "react";
import { useAuthContext } from "./useAuthContext";
import { axios } from "axios";

export const useLogIn = () => {
  const [error, setError] = useState(null);
  const [isPending, setIsPending] = useState(null);
  const { dispatch } = useAuthContext();

  const logIn = async (signInModel) => {
    setError(null);
    setIsPending(true);
    let sessResponse = await axios.post("/api/login", signInModel);
    if (sessResponse.status === 200) {
      dispatch({ type: "UPDATE_SESSION", payload: sessResponse.data });
      let userId = sessResponse.data.userId;
      let userResponse = await axios.get(`/api/users/${userId}`);
      dispatch({ type: "UPDATE_PROFILE", payload: userResponse.data });
      setIsPending(false);
      setError(null);
    } else {
      throw new Error(sessResponse);
    }

    try {
    } catch (error) {
      console.log(error);
      setError(error);
      setIsPending(false);
    }
  };

  return { error, isPending, logIn };
};
