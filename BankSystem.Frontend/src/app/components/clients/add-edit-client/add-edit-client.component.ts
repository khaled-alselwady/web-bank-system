import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { FormMode } from 'src/app/enums/form-mode.enum';
import { AddEditClient } from 'src/app/models/client/add-edit-client.model';
import { ClientsDataService } from 'src/app/services/clients-data.service';
import { ClientsService } from 'src/app/services/clients.service';
import { FormService } from 'src/app/services/form.service';
import { AlertComponent } from '../../shared/alert/alert.component';
import { HeaderItemService } from 'src/app/services/header-item.service';

@Component({
  selector: 'app-add-edit-client',
  templateUrl: './add-edit-client.component.html',
  styleUrls: ['./add-edit-client.component.scss'],
})
export class AddEditClientComponent implements OnInit, OnDestroy {
  clientInfoForm!: FormGroup;
  isPersonFormValid = false;
  personInfo: any;
  clientId?: number;
  private formMode: FormMode = FormMode.ADD;
  @ViewChild(AlertComponent) alertComponent!: AlertComponent;

  constructor(
    private fb: FormBuilder,
    private formService: FormService,
    private clientsDataService: ClientsDataService,
    private clientsService: ClientsService,
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private headerItemService: HeaderItemService
  ) {}
  ngOnDestroy(): void {
    this.headerItemService.headerItemName.next('Manage Clients');
  }

  ngOnInit(): void {
    this.clientInfoForm = this.fb.group({
      accountNumber: ['', Validators.required],
      pinCode: ['', [Validators.required, Validators.minLength(4)]],
      balance: ['', [Validators.required, Validators.min(0)]],
      isActive: [true],
    });

    this.activatedRoute.fragment.subscribe((fragment) => {
      if (fragment === 'adding') {
        this.formMode = FormMode.ADD;
        this.headerItemService.headerItemName.next('Add New Client');
      } else {
        this.formMode = FormMode.EDIT;
        this.headerItemService.headerItemName.next('Update New Client');
      }
    });

    if (this.formMode === FormMode.EDIT) {
      this.activatedRoute.params.subscribe({
        next: (params) => {
          this.clientId = params['clientId'];
          if (!this.clientId) {
            return;
          }
          this.clientsService
            .findByClientId(this.clientId)
            .subscribe((client) => {
              this.clientInfoForm.patchValue(client);
              this.formService.fillPersonData.next(client.person);
            });
        },
      });
    }
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
      if (this.formMode === FormMode.ADD) {
        this.clientsService.add(clientData).subscribe(
          () => {
            this.alertComponent?.show('Client saved successfully', 'success');
            this.headerItemService.headerItemName.next('Update New Client');
            this.formMode = FormMode.EDIT;
          },
          (err) => {
            this.alertComponent?.show('Failed to add client.', 'error');
          }
        );
      } else {
        if (!this.clientId) {
          return;
        }
        this.clientsService.update(this.clientId, clientData).subscribe(
          () => {
            this.alertComponent?.show('Client updated successfully', 'success');
          },
          (err) => {
            this.alertComponent?.show('Failed to update client.', 'error');
          }
        );
        // this.router.navigate(['../'], { relativeTo: this.activatedRoute }); // Navigate back to the clients list page after updating a client. If you want to keep the same page, you can use `this.router.navigate(['..', client.id], { relativeTo: this.activatedRoute });` instead.
      }
    } else {
      this.alertComponent?.show(
        'Please fill out all required fields.',
        'error'
      );
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
