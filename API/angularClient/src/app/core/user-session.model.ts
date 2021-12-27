export interface userSession {
  userId?: number;
  username?: string;
  sessionId?: number;
  sessionToken?: string;
  isAuthed: boolean;
}
