import { Person } from '../person/person.model';

export interface User {
  id: number;
  username: string;
  password: string;
  permissions: null;
  isActive: boolean;
  person: Person;
}