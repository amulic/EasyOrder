import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  private baseUrl: string = "https://localhost:7282/api/"
  constructor(private http:HttpClient) { }

  getUsers() {
    return this.http.get<any>(`${this.baseUrl}/User`);
  }
}
