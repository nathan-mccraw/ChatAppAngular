import { Injectable } from '@angular/core';
import { BehaviorSubject, Subject } from 'rxjs';
import { currentUserModel } from './models/current-user.model';

@Injectable({
  providedIn: 'root'
})
export class CurrentUserProfileService {
  currentUser: Subject<currentUserModel> = new BehaviorSubject(new currentUserModel);
  constructor() { }
}
