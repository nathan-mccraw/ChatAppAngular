import { Injectable } from "@angular/core";
import { BehaviorSubject, Subject } from "rxjs";
import { otherUsers } from "../models/user-profile.model";
import { usersHttpService } from "./users-http.service";

@Injectable({
    providedIn: 'root',
})

export class otherUsersDataService {
    otherUsers: Subject<otherUsers> = new BehaviorSubject(new otherUsers);

    constructor(private usersService: usersHttpService){}

    getOtherUsersArray(){
        this.usersService.getUsers().subscribe((data: any) => {
            console.log(`Attempted get other Users and received back:`)
            console.log(data);
        })
        
        //this.otherUsers.next(OtherUsersArray);
    }
}