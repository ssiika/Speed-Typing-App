import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { ServiceResponse, UserData } from '../../types';
import { Observable, of } from 'rxjs';
import { catchError, map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  API_URL = '/api/Users/';

  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
    })
  };

  constructor(
    private http: HttpClient
  ) { }

  // Login user
  login(userData: UserData): Observable<ServiceResponse<string>> {
    return this.http.post<ServiceResponse<string>>(this.API_URL + 'login', userData, this.httpOptions)
      .pipe(
        catchError(this.handleError())
      );
  }

  private handleError() {
    return (error: any): Observable<ServiceResponse<string>> => {

      const errorResponse: ServiceResponse<string> = {
        data: '',
        success: false,
        message: error.error.message ? error.error.message : error.message

      }

      console.log(errorResponse.message); // log to console 

      // Let the app keep running by returning an empty result.
      return of(errorResponse);
    };
  }
}
