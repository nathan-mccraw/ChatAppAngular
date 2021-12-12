import { useState } from "react";
import { useAuthContext } from "./useAuthContext";
import { axios } from "axios";

export const useModifyUserProfile = () => {
  const [error, setError] = useState(null);
  const [isPending, setIsPending] = useState(null);
  const { dispatch } = useAuthContext();

  const modifyUser = async (userProfileModel) => {
    setIsPending(true);
    setError(null);

    try {
      await axios.put("/api/signin", userProfileModel);
      dispatch({ type: "LOGOUT" });
      setIsPending(false);
      setError(null);
    } catch (error) {
      console.log(error.message);
      setError(error);
      setIsPending(false);
    }
  };

  return { modifyUser, error, isPending };
};
