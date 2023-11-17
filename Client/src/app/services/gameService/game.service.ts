import { Injectable } from '@angular/core';
import { ServiceResponse, Text } from '../../types';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { ErrorService } from '../errorService/error.service';
import { AuthService } from '../authService/auth.service';

@Injectable({
  providedIn: 'root'
})
export class GameService {

  constructor(
    private http: HttpClient,
    private errorService: ErrorService,
    private authService: AuthService
  ) { }

  private defaultText: string = 'Mwaaaah, the French... champagne has always been celebrated for its excellence\
    .There is a California champagne by Paul Masson, inspired... by that same French excellence.\
  It\'s fermented in the bottle, and like the best French champagnes, it\'s vintage-dated,\
  so Paul Masson\'s superb... (interrupted) '

  getDefaultText(): string {
    return this.defaultText
  }

  getText(): Observable<ServiceResponse<Text>> {
    const options = this.initOptions();
    return this.http.get<ServiceResponse<Text>>('/api/Text', options)
      .pipe(
        catchError(this.errorService.handleError<Text>())
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

