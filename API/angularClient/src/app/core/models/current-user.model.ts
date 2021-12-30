// export class currentUserModel{
//     userId: number = 0;
//     username: string = '';
//     sessionId: number = 0;
//     sessionToken: string = '';
//     firstName: string = '';
//     lastName: string = '';
//     location: string = '';
//     dateOfBirth: Date = new Date();
//     lastActive: Date = new Date();
//     hasOtherSessions: boolean = false;
//     isAuthorized: boolean = false;
// }

import { userProfile } from "./user-profile.model";
import { userSession } from "./user-session.model";

export class currentUserModel{
    profile: userProfile = new userProfile;
    session: userSession = new userSession;
    isAuthorized: boolean = false;
}