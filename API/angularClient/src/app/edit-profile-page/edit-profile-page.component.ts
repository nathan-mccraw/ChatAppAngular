import { Component} from '@angular/core';
import {FormControl, FormGroup} from '@angular/forms';

@Component({
  selector: 'app-edit-profile-page',
  templateUrl: './edit-profile-page.component.html',
  styleUrls: ['./edit-profile-page.component.css'],
})
export class EditProfilePageComponent{
  editDate: boolean = false;
  signUpForm: FormGroup = new FormGroup({
    username: new FormControl(null),
    password: new FormControl(null),
    firstName: new FormControl(null),
    lastName: new FormControl(null),
    location: new FormControl(null),
    dateOfBirth: new FormControl(null)
  });

}
