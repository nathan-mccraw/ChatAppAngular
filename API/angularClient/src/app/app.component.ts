import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { CurrentUserProfileService } from './core/services/current-user-profile.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent implements OnInit{
  constructor(private currentUser: CurrentUserProfileService){}

  ngOnInit(): void {
      //this.currentUser.validateUser();
  }
}
