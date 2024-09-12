import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { User } from '../models/user/user.model';
import { throwError } from 'rxjs/internal/observable/throwError';
import { catchError } from 'rxjs/operators';
import { Router } from '@angular/router';
import { UserView } from '../models/user/user-view.model';

@Injectable({ providedIn: 'root' })
export class UsersService {
  private baseUrl = 'http://localhost:5006/api/users/';
  currentUser: User | undefined = undefined;
  constructor(private http: HttpClient, private router: Router) {}

  fetchAll() {
    return this.http
      .get<UserView[]>(`${this.baseUrl}all`)
      .pipe(catchError(this.handleError));
  }

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

  count() {
    return this.http
      .get<number>(`${this.baseUrl}count`)
      .pipe(catchError(this.handleError));
  }

  pagerUsersByPageNumber(pageNumber: number, pageSize: number) {
    return this.http
      .get<UserView[]>(
        `${this.baseUrl}pageUsingPageNumber/${pageNumber}/${pageSize}`
      )
      .pipe(catchError(this.handleError));
  }

  signOut() {
    this.currentUser = undefined;
    this.router.navigate(['']);
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
