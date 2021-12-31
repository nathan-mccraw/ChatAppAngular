import { Component, OnInit } from '@angular/core';
import { CurrentUserProfileService } from '../core/services/current-user-profile.service';

@Component({
  selector: 'app-chat-page',
  templateUrl: './chat-page.component.html',
  styleUrls: ['./chat-page.component.css']
})
export class ChatPageComponent implements OnInit {

  constructor(private currentUserService: CurrentUserProfileService) { }

  ngOnInit(): void {
  }

  logUser(){
    console.log("clicked logger");
    let user = this.currentUserService.currentUser;

    console.log(user);
  }

}
