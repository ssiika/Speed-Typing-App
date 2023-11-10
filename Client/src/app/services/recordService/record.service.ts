import { Injectable } from '@angular/core';
import { Record, ServiceResponse, UserSafe } from '../../types';
import { Observable, of } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class RecordService {

  constructor(
    private http: HttpClient
  ) { }

  User1: UserSafe = {
    Id: 1,
    Username: 'User1'
  }

  User2: UserSafe = {
    Id: 2,
    Username: 'User2'
  }

  records: Record[] = [
    {
      id: 11,
      length: 'Short',
      time: 5,
      user: this.User1
    },
    {
      id: 12,
      length: 'Medium',
      time: 10,
      user: this.User1
    },
    {
      id: 13,
      length: 'Long',
      time: 15,
      user: this.User2
    },
  ];

  getRecords(): Observable<Record[]> {
    return of(this.records);
  }

  auth_token = 'eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTUxMiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoic3RldmVuIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvbmFtZWlkZW50aWZpZXIiOiI3IiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjoiVXNlciIsImV4cCI6MTY5OTY4MzM3N30.USauBIHwCqdjiX-Un7SvyuYzpwV3Qx0a7zM7MZ7GL9MYZhhCQm6LK2GHIGvBXuqfwsJmY87RU5Q-dK_17TQ5YQ'

  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${this.auth_token}`,
    })
  };

  getAsyncRecords(): Observable<ServiceResponse<Record[]>> {
    return this.http.get<ServiceResponse<Record[]>>('/api/Records', this.httpOptions)
      .pipe(
        catchError(this.handleError<Record[]>())
      );
  }

  private handleError<T>(){
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
