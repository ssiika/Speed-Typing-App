import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { ServiceResponse, UserData, UserCreds } from '../../types';
import { Observable, of } from 'rxjs';
import { catchError, map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  constructor(
    private http: HttpClient,
  ) { }

  API_URL = '/api/Users/';

  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
    })
  };

  userCheck(): string {
    const localUser = localStorage.getItem('user');

    return localUser ? localUser : '';
  }

  // Login user
  login(userData: UserData): Observable<ServiceResponse<UserCreds>> {
    return this.http.post<ServiceResponse<UserCreds>>(this.API_URL + 'login', userData, this.httpOptions)
      .pipe(
        catchError(this.handleError<UserCreds>())
      );
  }

  register(userData: UserData): Observable<ServiceResponse<UserCreds>> {
    return this.http.post<ServiceResponse<UserCreds>>(this.API_URL, userData, this.httpOptions)
      .pipe(
        catchError(this.handleError<UserCreds>())
      );
  }

  private handleError<T>() {
    return (error: any): Observable<ServiceResponse<T>> => {

      const errorResponse: ServiceResponse<T> = {
        success: false,
        message: (error.error && error.error.message)
          ? error.error.message
          : error.message
      }

      console.log(errorResponse.message); // log to console 

      // Let the app keep running by returning an empty result.
      return of(errorResponse);
    };
  }
}
