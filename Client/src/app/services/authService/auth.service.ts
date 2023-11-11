import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { ServiceResponse, UserData, UserCreds } from '../../types';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { ErrorService } from '../errorService/error.service'
import { jwtDecode } from "jwt-decode";

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  constructor(
    private http: HttpClient,
    private errorService: ErrorService
  ) { }

  API_URL = '/api/Users/';

  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
    })
  };

  getValidUsername(): string {
    const localUser = localStorage.getItem('user');

    // Check if token is expired
    if (localUser) {
      const parsedDetails = JSON.parse(localUser)

      const expiryDate = jwtDecode(parsedDetails.token).exp

      if (expiryDate && Date.now() <= expiryDate * 1000) {
        return parsedDetails.username;
      }
    }
    // No token or token expired, return falsy string
    return '';
  }

  // Login user
  login(userData: UserData): Observable<ServiceResponse<UserCreds>> {
    return this.http.post<ServiceResponse<UserCreds>>(this.API_URL + 'login', userData, this.httpOptions)
      .pipe(
        catchError(this.errorService.handleError<UserCreds>())
      );
  }

  register(userData: UserData): Observable<ServiceResponse<UserCreds>> {
    return this.http.post<ServiceResponse<UserCreds>>(this.API_URL, userData, this.httpOptions)
      .pipe(
        catchError(this.errorService.handleError<UserCreds>())
      );
  }
}
