export interface userSession {
  id?: number;
  userId?: number;
  userToken?: string;
  hasOtherActiveSessions: boolean;
}
