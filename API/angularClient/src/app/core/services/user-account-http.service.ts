import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { userAccountFormData } from '../interfaces/user-account.model';
import { signInFormData } from '../interfaces/sign-in.model';
import { userSession } from '../models/user-session.model';

@Injectable({
  providedIn: 'root',
})
export class userAccountHttpService {
  baseURL: string = "https://localhost:5001/api/";
  constructor(private http: HttpClient) {}

  validateUser(){
    return this.http.get<userSession>(this.baseURL + "validateUser");
  }

  signIn(signInData: signInFormData) {
    return this.http.post<userSession>(this.baseURL + "signIn", signInData);
  }

  signUpUser(newUser: userAccountFormData) {
    return this.http.post<userSession>(this.baseURL + "signUp", newUser);
  }

  signUpGuest() {
    return this.http.get<userSession>(this.baseURL + "signUp");
  }

  editUser(userAccount: userAccountFormData) {
    return this.http.put<userSession>(this.baseURL + "signIn", userAccount);
  }

  // deleteUser(signInData: signInFormData) {
  //   return this.http.delete(`api/signIn`);
  // }
}
