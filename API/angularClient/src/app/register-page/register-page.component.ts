import { Component, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { userAccountFormData } from '../core/interfaces/user-account.model';
import { CurrentUserProfileService } from '../core/services/current-user-profile.service';

@Component({
  selector: 'app-register-page',
  templateUrl: './register-page.component.html',
  styleUrls: ['./register-page.component.css']
})
export class RegisterPageComponent implements OnInit {
  newUser: userAccountFormData = {
    username: '',
    password: '',
    firstName: '',
    lastName: '',
    location: '',
    dateOfBirth: new Date
  };

  constructor(private userProfileService: CurrentUserProfileService) { }
  ngOnInit(): void {
  }

  onRegister(formData: NgForm){
    console.log(formData);
    this.newUser.username = formData.value.username;
    this.newUser.password = formData.value.password;
    this.newUser.firstName = formData.value.firstName;
    this.newUser.lastName = formData.value.lastName;
    this.newUser.location = formData.value.location;
    this.newUser.dateOfBirth = formData.value.dateOfBirth;

    this.userProfileService.signUp(this.newUser);
  }

}
