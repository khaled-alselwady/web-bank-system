import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-add-update-person',
  templateUrl: './add-update-person.component.html',
  styleUrls: ['./add-update-person.component.scss'],
})
export class AddUpdatePersonComponent implements OnInit {
  personalInfoForm?: FormGroup;

  constructor(private fb: FormBuilder) {}

  ngOnInit() {
    this.personalInfoForm = this.fb.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      gender: ['Male', Validators.required], // Ensure `gender` control is present
      phone: ['', [Validators.required, Validators.pattern(/^\+?\d+$/)]],
      email: ['', [Validators.required, Validators.email]],
    });
  }
}