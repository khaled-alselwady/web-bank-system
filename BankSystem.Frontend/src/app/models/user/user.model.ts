import { Person } from '../person/person.model';

export interface User {
  id: number;
  username: string;
  password: string;
  permissions: number;
  isActive: boolean;
  person: Person;
}
