import { Injectable } from "@angular/core";
import { BehaviorSubject, ReplaySubject, Subject } from "rxjs";
import { userProfile } from "../models/user-profile.model";
import { usersHttpService } from "./users-http.service";

@Injectable({
    providedIn: 'root',
})

export class otherUsersDataService {
    otherUsers: Subject<userProfile[]> = new ReplaySubject();

    constructor(private usersService: usersHttpService){}

    getOtherUsersArray(){
        this.usersService.getUsers().subscribe((data) => {
            this.otherUsers.next(data)
            console.log(`Attempted get other Users and received back:`)
            console.log(data);
        })
    }
}