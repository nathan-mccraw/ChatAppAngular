import { userSession } from "../models/user-session.model";

export interface userAccountFormData {
  userSession?: userSession;
  username: string;
  password: string;
  newPassword?: string;
  firstName?: string;
  lastName?: string;
  location?: string;
  dateOfBirth?: Date;
}
