import { Injectable } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Subject } from 'rxjs';
import { Person } from '../models/person/person.model';

@Injectable({ providedIn: 'root' })
export class FormService {
  resetFields = new Subject<void>();
  fillPersonData = new Subject<Person>();

  constructor(private router: Router, private activatedRoute: ActivatedRoute) {}
}
