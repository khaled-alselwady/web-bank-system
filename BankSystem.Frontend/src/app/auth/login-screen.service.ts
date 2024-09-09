import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class LoginScreenService {
  private showLoginScreenSubject = new BehaviorSubject<boolean>(false);
  showLoginScreen$ = this.showLoginScreenSubject.asObservable();

  // Method to change the state
  setShowLoginScreen(value: boolean) {
    this.showLoginScreenSubject.next(value);
  }
}
