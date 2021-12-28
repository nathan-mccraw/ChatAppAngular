export class currentUserModel{
    userId: number = 0;
    username: string = '';
    sessionId: number = 0;
    sessionToken: string = '';
    firstName: string = '';
    lastName: string = '';
    location: string = '';
    dateOfBirth: Date = new Date();
    lastActive: Date = new Date();
    hasOtherSessions: boolean = false;
    isAuthorized: boolean = false;
}