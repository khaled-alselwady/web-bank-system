import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Subscription } from 'rxjs';
import { take } from 'rxjs/operators';
import { FormService } from 'src/app/services/form.service';

@Component({
  selector: 'app-add-edit-person',
  templateUrl: './add-edit-person.component.html',
  styleUrls: ['./add-edit-person.component.scss'],
})
export class AddEditPersonComponent implements OnInit, OnDestroy {
  personalInfoForm?: FormGroup;
  resetSub?: Subscription;

  constructor(private fb: FormBuilder, private formService: FormService) {}

  ngOnInit() {
    this.personalInfoForm = this.fb.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      gender: ['Male', Validators.required], // Ensure `gender` control is present
      phone: ['', [Validators.required, Validators.pattern(/^\+?\d+$/)]],
      email: ['', [Validators.email]],
    });

    this.resetSub = this.formService.resetFields.pipe(take(1)).subscribe({
      next: () => {
        this.personalInfoForm?.reset();
      },
    });
  }

  ngOnDestroy(): void {
    this.resetSub?.unsubscribe();
  }
}
