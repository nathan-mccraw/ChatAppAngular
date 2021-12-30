import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { userProfile } from '../models/user-profile.model';

@Injectable({
  providedIn: 'root',
})
export class usersHttpService {
  baseURL: string = "https://localhost:5001/api/";
  constructor(private http: HttpClient) {}

  getUser(id: number) {
    return this.http.get<userProfile>(this.baseURL + "users/" + id);
  }

  getUsers() {
    return this.http.get(`api/users`);
  }
}
