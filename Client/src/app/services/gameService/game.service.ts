import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class GameService {

  constructor() { }

  private defaultText: string = 'Mwaaaah, the French... champagne has always been celebrated for its excellence\
    .There is a California champagne by Paul Masson, inspired... by that same French excellence.\
  It\'s fermented in the bottle, and like the best French champagnes, it\'s vintage-dated,\
  so Paul Masson\'s superb... (interrupted) '

  getDefaultText(): string {
    return this.defaultText
  }

  getApiText() {
    return false;
  }
}

