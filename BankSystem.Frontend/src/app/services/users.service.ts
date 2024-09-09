import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { User } from '../user/user.model';
import { throwError } from 'rxjs/internal/observable/throwError';
import { catchError } from 'rxjs/operators';

@Injectable({ providedIn: 'root' })
export class UsersService {
  private baseUrl = 'http://localhost:5006/api/users/';
  constructor(private http: HttpClient) {}

  findUserByUsernameAndPassword(username: string, password: string) {
    return this.http
      .get<User>(
        `${this.baseUrl}findByUsernameAndPassword/${username}/${password}`
      )
      .pipe(catchError(this.handleError));
  }

  existsUserByUsernameAndPassword(username: string, password: string) {
    return this.http
      .get<boolean>(
        `${this.baseUrl}existsByUsernameAndPassword/${username}/${password}`
      )
      .pipe(catchError(this.handleError));
  }

  isUserActive(id: number) {
    return this.http
      .get<boolean>(`${this.baseUrl}isActive/${id}`)
      .pipe(catchError(this.handleError));
  }

  // Error handling
  private handleError(error: HttpErrorResponse) {
    let errorMessage = 'Unknown error!';

    if (!error || !error.error || !error.error.message) {
      return throwError(errorMessage);
    }

    errorMessage = error.error.message;
    return throwError(errorMessage);
  }
}
