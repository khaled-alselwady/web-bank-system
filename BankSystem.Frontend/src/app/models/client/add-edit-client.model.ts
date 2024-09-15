import { AddEditPerson } from '../person/add-edit-person.model';

export interface AddEditClient {
  accountNumber: string;
  pinCode: string;
  balance: number;
  isActive: boolean;
  person: AddEditPerson;
}
