import { Component, OnInit } from '@angular/core';
import { CurrentUserProfileService } from '../core/services/current-user-profile.service';

@Component({
  selector: 'app-sign-in-page',
  templateUrl: './sign-in-page.component.html',
  styleUrls: ['./sign-in-page.component.css']
})
export class SignInPageComponent implements OnInit {

  constructor( private currentUserService: CurrentUserProfileService) { }

  ngOnInit(): void {
  }

  guestSignUp(){
    this.currentUserService.signUpAsGuest();
    console.log("clicked guestSignUp")
    let user = this.currentUserService.currentUser;
    console.log(user);
  }

  logUser(){
    console.log("clicked logger");
    let user = this.currentUserService.currentUser;

    console.log(user);
  }

}
