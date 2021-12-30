export class userProfile{
    id: number = 0;
    username: string = '';
    firstName: string = '';
    lastName: string = '';
    location: string = '';
    dateOfBirth: Date = new Date;
    dateCreated: Date = new Date;
    lastActive: Date = new Date;
}

export class otherUsers{
    isDataLoading: boolean = false;
    profiles: userProfile[] = [];
}