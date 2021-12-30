import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { userAccountFormData } from '../core/interfaces/user-account.model';

@Component({
  selector: 'app-register-page',
  templateUrl: './register-page.component.html',
  styleUrls: ['./register-page.component.css']
})
export class RegisterPageComponent implements OnInit {
  newUser: userAccountFormData = {
    username: '',
    currentPassword: '',
    firstName: '',
    lastName: '',
    location: '',
    dateOfBirth: new Date
  };
  
  constructor() { }

  ngOnInit(): void {
  }

  onRegister(formData: NgForm){
    console.log(formData);
  }

}
