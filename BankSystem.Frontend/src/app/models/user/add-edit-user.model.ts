import { AddEditPerson } from '../person/add-edit-person.model';

export interface AddEditUser {
  username: string;
  password: string;
  permissions: number;
  isActive: boolean;
  person: AddEditPerson;
}
