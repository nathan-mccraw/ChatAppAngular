import { Injectable } from '@angular/core';
import { BehaviorSubject, Subject } from 'rxjs';
import { signInFormData } from '../interfaces/sign-in.model';
import { userAccountFormData } from '../interfaces/user-account.model';
import { currentUserModel } from '../models/current-user.model';
import { userAccountHttpService } from './user-account-http.service';
import { usersHttpService } from './users-http.service';

@Injectable({
  providedIn: 'root'
})
export class CurrentUserProfileService {
  currentUser: Subject<currentUserModel> = new BehaviorSubject(new currentUserModel);

  constructor(private userAccountService: userAccountHttpService, private usersService: usersHttpService) { }

  validateUser(){
    let user: currentUserModel = new currentUserModel();
    this.userAccountService.validateUser()
      .subscribe(data => {
        user.session = data;
        this.usersService.getUser(user.session.userId)
          .subscribe(data => {
            user.profile = data;
        });
      });

    user.isAuthorized = true;
    this.currentUser.next(user);
  }

  signIn(formData: signInFormData){
    let user: currentUserModel = new currentUserModel();
    this.userAccountService.signIn(formData).subscribe(data => {
        user.session = data;
      });
    
    this.usersService.getUser(user.session.userId).subscribe(data => {
        user.profile = data;
      });
    
    user.isAuthorized = true;
    this.currentUser.next(user);
  }

  signUp(formData: userAccountFormData){
    let user: currentUserModel = new currentUserModel();
    this.userAccountService.signUpUser(formData).subscribe(data => {
        user.session = data;
      });
    
    this.usersService.getUser(user.session.userId).subscribe(data => {
        user.profile = data;
      });
    
    user.isAuthorized = true;
    this.currentUser.next(user);
  }

  signUpAsGuest(){
    let user: currentUserModel = new currentUserModel();
    this.userAccountService.signUpGuest().subscribe(data => {
      user.session = data;

      this.usersService.getUser(user.session.userId).subscribe(data => {
        user.profile = data;
      });
  
      user.isAuthorized = true;
      this.currentUser.next(user);
    });
  }

  editUserProfile(formData: userAccountFormData){
    let user: currentUserModel = new currentUserModel();
    this.userAccountService.editUser(formData).subscribe(data => {
      user.session = data;
    });
  
    this.usersService.getUser(user.session.userId).subscribe(data => {
        user.profile = data;
      });
  
    user.isAuthorized = true;
    this.currentUser.next(user);
  }

  // deleteUser(){
  //   this.userAccountService.deleteUser
  // }
}
