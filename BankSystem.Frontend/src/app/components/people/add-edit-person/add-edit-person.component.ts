import {
  Component,
  EventEmitter,
  OnDestroy,
  OnInit,
  Output,
} from '@angular/core';
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
  private subscription?: Subscription[] = [];
  @Output() valid = new EventEmitter<{ isValid: boolean; personInfo: any }>();

  constructor(private fb: FormBuilder, private formService: FormService) {}

  ngOnInit() {
    this.personalInfoForm = this.fb.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      gender: ['Male', Validators.required], // Ensure `gender` control is present
      phone: ['', [Validators.required, Validators.pattern(/^\+?\d+$/)]],
      email: ['', [Validators.email]],
    });

    this.subscription?.push(this.subToCheckPersonFormStatus());

    this.subscription?.push(this.subToFillFields());

    this.subscription?.push(this.subToResetFields());
  }

  ngOnDestroy(): void {
    this.subscription?.forEach((sub) => sub.unsubscribe());
  }

  private subToCheckPersonFormStatus() {
    return this.personalInfoForm!.statusChanges.subscribe((status) => {
      if (status === 'VALID') {
        this.valid.emit({
          isValid: true,
          personInfo: this.personalInfoForm?.value,
        });
      } else {
        this.valid.emit({
          isValid: false,
          personInfo: this.personalInfoForm?.value,
        });
      }
    });
  }

  private subToFillFields() {
    return this.formService.fillPersonData.subscribe((data) => {
      this.personalInfoForm?.patchValue(data);
    });
  }

  private subToResetFields() {
    return this.formService.resetFields.pipe(take(1)).subscribe({
      next: () => {
        this.personalInfoForm?.reset();
      },
    });
  }
}
