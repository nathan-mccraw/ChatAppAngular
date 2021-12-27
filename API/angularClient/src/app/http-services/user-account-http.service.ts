import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { userAccount } from './../core/user-account.model';
import { signIn } from './../core/sign-in.model';

@Injectable({
  providedIn: 'root',
})
export class userAccountHttpService {
  constructor(private http: HttpClient) {}

  signIn(signInData: signIn) {
    return this.http.post(`api/signin`, signInData);
  }

  signUpUser(newUser: userAccount) {
    return this.http.post(`api/signup`, newUser);
  }

  signUpGuest() {
    return this.http.get('api/signup');
  }

  editUser(userAccount: userAccount) {
    return this.http.put('api/signin', userAccount);
  }

  deleteUser() {
    return this.http.delete(`api/signin`);
  }
}
