import { Injectable } from '@angular/core';
import { Record, ServiceResponse } from '../../types';
import { Observable } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { catchError } from 'rxjs/operators';
import { ErrorService } from '../errorService/error.service'
import { AuthService } from '../authService/auth.service'

@Injectable({
  providedIn: 'root'
})
export class RecordService {

  constructor(
    private http: HttpClient,
    private errorService: ErrorService,
    private authService: AuthService
  ) { }

  getRecords(): Observable<ServiceResponse<Record[]>> {
    const options = this.initOptions();
    return this.http.get<ServiceResponse<Record[]>>('/api/Records', options)
      .pipe(
        catchError(this.errorService.handleError<Record[]>())
      );
  }

  private initOptions(): Object {
    const token = this.authService.getToken();

    return {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${token}`,
      })
    };
  }
}
