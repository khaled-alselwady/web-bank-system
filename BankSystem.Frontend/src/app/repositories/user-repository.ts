import { Observable, of } from 'rxjs';
import { FormMode } from '../enums/form-mode.enum';
import { catchError, map } from 'rxjs/operators';
import { User } from '../models/user/user.model';
import { AddEditUser } from '../models/user/add-edit-user.model';
import { UsersService } from '../services/users.service';

export class UserRepository {
  mode: FormMode = FormMode.ADD;
  userData: User;
  userDataForAddAndUpdate!: AddEditUser;

  constructor(private usersService: UsersService) {
    this.mode = FormMode.ADD;
    this.userData = {
      id: 0,
      username: '',
      password: '',
      permissions: 0,
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
    return this.usersService.add(this.userDataForAddAndUpdate).pipe(
      map((newUser) => {
        this.userData = newUser;
        return true; // Return true on success
      }),
      catchError((error) => {
        console.error('Error adding user:', error);
        return of(false); // Return false on error
      })
    );
  }

  private update() {
    return this.usersService
      .update(this.userData.id, this.userDataForAddAndUpdate)
      .pipe(
        map(() => {
          return true; // Return true on success
        }),
        catchError((error) => {
          console.error('Error adding user:', error);
          return of(false); // Return false on error
        })
      );
  }

  findByClientId(id: number) {
    return this.usersService.findByUserId(id).pipe(
      map((userData) => {
        this.userData = userData;
        this.mode = FormMode.EDIT;
        return userData; // Return true on success
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
}
