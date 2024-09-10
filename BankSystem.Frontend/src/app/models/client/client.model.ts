import { Person } from '../person/person.model';

export interface Client {
  id: number;
  accountNumber: string;
  pinCode: string;
  balance: number;
  isActive: boolean;
  person: Person;
}
