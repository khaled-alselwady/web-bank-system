import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ClientsDataService } from 'src/app/services/clients-data.service';
import { ClientsService } from 'src/app/services/clients.service';
import { FormService } from 'src/app/services/form.service';
import { AlertComponent } from '../../shared/alert/alert.component';
import { HeaderItemService } from 'src/app/services/header-item.service';
import { ClientRepository } from 'src/app/repositories/client-repository';

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
  @ViewChild(AlertComponent) alertComponent!: AlertComponent;

  clientRepository: ClientRepository = new ClientRepository(
    this.clientsService
  );

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

    this.activatedRoute.params.subscribe({
      next: (params) => {
        this.clientId = params['clientId'];
        this.fillClientFields();
      },
    });

    this.activatedRoute.fragment.subscribe((fragment) => {
      if (fragment === 'adding') {
        this.headerItemService.headerItemName.next('Add New Client');
        this.clientRepository = new ClientRepository(this.clientsService);
      } else {
        this.headerItemService.headerItemName.next('Update New Client');
        this.clientRepository.findByClientId(this.clientId!).subscribe();
      }
    });
  }

  private fillClientFields() {
    if (!this.clientId) {
      return;
    }
    this.clientRepository.findByClientId(this.clientId).subscribe((client) => {
      this.clientInfoForm.patchValue(client);
      this.formService.fillPersonData.next(client.person);
    });
  }

  // You can create a method to handle form submission
  onSubmit(): void {
    if (this.clientInfoForm.valid && this.isPersonFormValid) {
      this.clientRepository.clientDataForAddAndUpdate = {
        accountNumber: this.clientInfoForm.value.accountNumber,
        pinCode: this.clientInfoForm.value.pinCode,
        balance: this.clientInfoForm.value.balance,
        isActive: this.clientInfoForm.value.isActive,
        person: this.personInfo,
      };

      this.saveClient();
    }
  }

  private saveClient() {
    this.clientRepository.save().subscribe(
      (result) => {
        this.alertComponent?.show('Client saved successfully', 'success');
        this.headerItemService.headerItemName.next('Update New Client');
      },
      (err) => {
        this.alertComponent?.show('Failed to save client.', 'error');
      }
    );
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
