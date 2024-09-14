import { Injectable } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Subject } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class FormService {
  resetFields = new Subject<void>();

  constructor(private router: Router, private activatedRoute: ActivatedRoute) {}
}
