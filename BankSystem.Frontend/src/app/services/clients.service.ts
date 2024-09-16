import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { ClientView } from '../models/client/client-view.model';
import { throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { AddEditClient } from '../models/client/add-edit-client.model';
import { Client } from '../models/client/client.model';

@Injectable({ providedIn: 'root' })
export class ClientsService {
  private baseUrl = 'http://localhost:5006/api/clients/';
  constructor(private http: HttpClient, private router: Router) {}

  fetchAll() {
    return this.http
      .get<ClientView[]>(`${this.baseUrl}all`)
      .pipe(catchError(this.handleError));
  }

  count() {
    return this.http
      .get<number>(`${this.baseUrl}count`)
      .pipe(catchError(this.handleError));
  }

  pagerClientsByPageNumber(pageNumber: number, pageSize: number) {
    return this.http
      .get<ClientView[]>(
        `${this.baseUrl}pageUsingPageNumber/${pageNumber}/${pageSize}`
      )
      .pipe(catchError(this.handleError));
  }

  add(clientData: AddEditClient) {
    return this.http
      .post<Client>(`${this.baseUrl}`, clientData)
      .pipe(catchError(this.handleError));
  }

  delete(id: number) {
    return this.http
      .delete(`${this.baseUrl}${id}`)
      .pipe(catchError(this.handleError));
  }

  findByClientId(id: number) {
    return this.http
      .get<Client>(`${this.baseUrl}findByClientId/${id}`)
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
