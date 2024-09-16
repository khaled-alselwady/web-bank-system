import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { FormMode } from 'src/app/enums/form-mode.enum';
import { AddEditClient } from 'src/app/models/client/add-edit-client.model';
import { ClientsDataService } from 'src/app/services/clients-data.service';
import { ClientsService } from 'src/app/services/clients.service';
import { FormService } from 'src/app/services/form.service';

@Component({
  selector: 'app-add-edit-client',
  templateUrl: './add-edit-client.component.html',
  styleUrls: ['./add-edit-client.component.scss'],
})
export class AddEditClientComponent implements OnInit {
  clientInfoForm!: FormGroup;
  isPersonFormValid = false;
  personInfo: any;
  private formMode: FormMode = FormMode.ADD;

  constructor(
    private fb: FormBuilder,
    private formService: FormService,
    private clientsDataService: ClientsDataService,
    private clientsService: ClientsService,
    private router: Router,
    private activatedRoute: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.clientInfoForm = this.fb.group({
      accountNumber: ['', Validators.required],
      pinCode: ['', [Validators.required, Validators.minLength(4)]],
      balance: ['', [Validators.required, Validators.min(0)]],
      isActive: [true],
    });
  }

  // You can create a method to handle form submission
  onSubmit(): void {
    if (this.clientInfoForm.valid && this.isPersonFormValid) {
      const clientData: AddEditClient = {
        accountNumber: this.clientInfoForm.value.accountNumber,
        pinCode: this.clientInfoForm.value.pinCode,
        balance: this.clientInfoForm.value.balance,
        isActive: this.clientInfoForm.value.isActive,
        person: this.personInfo,
      };
      this.clientsService.add(clientData).subscribe((newClient) => {
        this.onCancel();
      });
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

  onPersonFormStatusChanged(event: { isValid: boolean; personInfo: any }) {
    this.isPersonFormValid = event.isValid;
    this.personInfo = event.personInfo;
  }
}
