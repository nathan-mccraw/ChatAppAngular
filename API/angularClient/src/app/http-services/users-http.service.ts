import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class usersHttpService {
  constructor(private http: HttpClient) {}

  getUser(id: number) {
    return this.http.get(`api/users/${id}`);
  }

  getUsers() {
    return this.http.get(`api/users`);
  }
}
