import { Injectable, Inject } from '@angular/core';
import { IUserLogin } from '../models/iuser-login';
import { Observable } from 'rxjs';
import { AppUserAuth } from '../models/app-user-auth';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { map, catchError } from 'rxjs/operators';
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  port = '44399';
  //baseUrl = `${this.window.location.protocol}//${this.window.location.hostname}:${this.port}`;
  baseUrl = `http://${this.window.location.hostname}:${this.port}`;
  redirectUrl: string;
  isAuthenticated = false;
  authUrl = this.baseUrl + '/api/users';
  appUserAuth = new AppUserAuth();
  private jwtHelper = new JwtHelperService();

  constructor(
    private http: HttpClient,
    @Inject('Window') private window: Window
  ) {
    this.appUserAuth = this.getUserLoggedIn();
    this.setUserLoggedIn(this.appUserAuth);
  }

  login(userLogin: IUserLogin): Observable<AppUserAuth> {
    return this.http.post<AppUserAuth>(this.authUrl, userLogin)
      .pipe(
        map(appUserAuth => {
          this.setUserLoggedIn(appUserAuth);
          return appUserAuth;
        }),
        catchError(this.handleError)
      );
  }

  private handleError(error: HttpErrorResponse) {
    console.error('server error:', error);
    if (error.error instanceof Error) {
      const errMessage = error.error.message;
      return Observable.throw(errMessage);
      // Use the following instead if using lite-server
      // return Observable.throw(err.text() || 'backend server error');
    }
    return Observable.throw(error || 'Server error');
  }

  setUserLoggedIn(appUserAuth: AppUserAuth) {
    sessionStorage.setItem('currentUser', JSON.stringify(appUserAuth));

    const isExpired = this.jwtHelper.isTokenExpired(appUserAuth.BearerToken);

    this.isAuthenticated = appUserAuth.IsAuthenticated && !isExpired;
    this.appUserAuth = appUserAuth;
  }

  getUserLoggedIn(): AppUserAuth {
    let appUserAuth = JSON.parse(sessionStorage.getItem('currentUser'));
    appUserAuth = appUserAuth || new AppUserAuth();
    return appUserAuth;
  }
}
