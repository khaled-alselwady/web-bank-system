import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-add-edit-person',
  templateUrl: './add-edit-person.component.html',
  styleUrls: ['./add-edit-person.component.scss'],
})
export class AddEditPersonComponent implements OnInit {
  personalInfoForm?: FormGroup;

  constructor(private fb: FormBuilder) {}

  ngOnInit() {
    this.personalInfoForm = this.fb.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      gender: ['Male', Validators.required], // Ensure `gender` control is present
      phone: ['', [Validators.required, Validators.pattern(/^\+?\d+$/)]],
      email: ['', [Validators.email]],
    });
  }
}
