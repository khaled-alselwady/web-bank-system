import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ClientsDataService } from 'src/app/services/clients-data.service';
import { FormService } from 'src/app/services/form.service';

@Component({
  selector: 'app-add-edit-client',
  templateUrl: './add-edit-client.component.html',
  styleUrls: ['./add-edit-client.component.scss'],
})
export class AddEditClientComponent implements OnInit {
  clientInfoForm!: FormGroup;

  constructor(
    private fb: FormBuilder,
    private formService: FormService,
    private clientsDataService: ClientsDataService,
    private router: Router,
    private activatedRoute: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.clientInfoForm = this.fb.group({
      accountNumber: ['', Validators.required],
      pinCode: ['', [Validators.required, Validators.minLength(4)]],
      balance: ['', [Validators.required, Validators.min(0)]],
      isActive: [true], // Default value set to 'true'
    });
  }

  // You can create a method to handle form submission
  onSubmit(): void {
    if (this.clientInfoForm.valid) {
      console.log(this.clientInfoForm.value);
      // Perform further actions such as API calls, etc.
    } else {
      this.clientInfoForm.markAllAsTouched(); // This will trigger validation messages
    }
  }

  onReset() {
    this.formService.resetFields.next();
  }

  onCancel() {
    this.clientsDataService.refreshClients$.next(); // raise the event to call subscribers
    this.router.navigate(['../'], { relativeTo: this.activatedRoute });
  }
}
