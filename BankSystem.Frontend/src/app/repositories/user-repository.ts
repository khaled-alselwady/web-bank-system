import { Observable, of } from 'rxjs';
import { FormMode } from '../enums/form-mode.enum';
import { AddEditClient } from '../models/client/add-edit-client.model';
import { Client } from '../models/client/client.model';
import { ClientsService } from '../services/clients.service';
import { catchError, map } from 'rxjs/operators';

export class UserRepository {
  mode: FormMode = FormMode.ADD;
  clientData: Client;
  clientDataForAddAndUpdate!: AddEditClient;

  constructor(private clientsService: ClientsService) {
    this.mode = FormMode.ADD;
    this.clientData = {
      id: 0,
      accountNumber: '',
      pinCode: '',
      balance: 0,
      isActive: true,
      person: {
        id: 0,
        firstName: '',
        lastName: '',
        email: '',
        phone: '',
        fullName: '',
        gender: '',
      },
    };
  }

  add(): Observable<boolean> {
    return this.clientsService.add(this.clientDataForAddAndUpdate).pipe(
      map((newClient) => {
        this.clientData = newClient;
        return true; // Return true on success
      }),
      catchError((error) => {
        console.error('Error adding client:', error);
        return of(false); // Return false on error
      })
    );
  }

  private update() {
    return this.clientsService
      .update(this.clientData.id, this.clientDataForAddAndUpdate)
      .pipe(
        map(() => {
          return true; // Return true on success
        }),
        catchError((error) => {
          console.error('Error adding client:', error);
          return of(false); // Return false on error
        })
      );
  }

  findByClientId(id: number) {
    return this.clientsService.findByClientId(id).pipe(
      map((clientData) => {
        this.clientData = clientData;
        this.mode = FormMode.EDIT;
        return clientData; // Return true on success
      })
    );
  }

  save() {
    switch (this.mode) {
      case FormMode.ADD:
        return this.add();
      case FormMode.EDIT:
        return this.update();
    }
  }

  delete(id: number) {
    this.clientsService.delete(id).subscribe(() => {});
  }
}
