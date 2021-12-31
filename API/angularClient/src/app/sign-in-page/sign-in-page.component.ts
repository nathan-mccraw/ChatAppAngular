import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { signInFormData } from '../core/interfaces/sign-in.model';
import { CurrentUserProfileService } from '../core/services/current-user-profile.service';
import { otherUsersDataService } from '../core/services/other-users.service';

@Component({
  selector: 'app-sign-in-page',
  templateUrl: './sign-in-page.component.html',
  styleUrls: ['./sign-in-page.component.css']
})
export class SignInPageComponent implements OnInit {
  signInForm: signInFormData = {
    username: '',
    password: ''
  };

  constructor( private currentUserService: CurrentUserProfileService,
               private otherUsersService: otherUsersDataService) { }

  ngOnInit(): void {
  }

  onLogin(formData: NgForm){
    this.signInForm.username = formData.value.username;
    this.signInForm.password = formData.value.password;

    this.currentUserService.signIn(this.signInForm);
  }

  guestSignUp(){
    this.currentUserService.signUpAsGuest();
    
    let user = this.currentUserService.currentUser;
    console.log(user);
  }

  logUser(){
    console.log("clicked logger");

    let user = this.currentUserService.currentUser;
    console.log(user);
  }

  getOtherUsers(){
    this.otherUsersService.getOtherUsersArray();

    let otherUsers = this.otherUsersService.otherUsers;
    console.log(otherUsers);
  }

}
