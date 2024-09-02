import { isPlatformBrowser } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { JwtHelperService } from "@auth0/angular-jwt"
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { toUSVString } from 'util';
import { IUser } from '../../models/IUser';
@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private baseUrl:string = "https://localhost:7282/api/User";
  private userSubject$ = new BehaviorSubject<IUser | null>(null);
  public user$:Observable<IUser | null>;
  
  constructor(private http: HttpClient) {
    this.userSubject$ = new BehaviorSubject<IUser | null>(null);
    this.user$ = this.userSubject$.asObservable();
  }

  signUp(userObj: any) {
    return this.http.post<any>(`${this.baseUrl}/register`, userObj);
  }

  signIn(loginObj: any) {
    return this.http.post<any>(`${this.baseUrl}/authenticate`, loginObj, {withCredentials:true});
  }
  
  getMe() : Observable<IUser> {
    return this.http.get<IUser>(`${this.baseUrl}/me`, { withCredentials: true }).pipe(
      tap((user: IUser) => {
        console.log("User fetched from /me endpoint:", user);
        this.userSubject$.next(user);
        console.log("Current userSubject$ value:", this.userSubject$.value);
      }, 
      (error) => {
        console.error("Error fetching user from /me endpoint:", error);
      })
    );
  }
  
  signOut() : Observable<any> {
    return this.http.post<any>(`${this.baseUrl}/logout`,null,{withCredentials:true}).pipe(tap(()=>{
      this.userSubject$.next(null);
    }));
    //maybe route to login
  }

  isLoggedIn(): boolean {
    return this.userSubject$.value !== null;
  }

}