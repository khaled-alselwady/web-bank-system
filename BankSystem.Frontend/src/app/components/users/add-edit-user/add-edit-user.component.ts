import { Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AlertComponent } from '../../shared/alert/alert.component';
import { UserRepository } from 'src/app/repositories/user-repository';
import { FormService } from 'src/app/services/form.service';
import { UsersService } from 'src/app/services/users.service';
import { ActivatedRoute, Router } from '@angular/router';
import { HeaderItemService } from 'src/app/services/header-item.service';

@Component({
  selector: 'app-add-edit-user',
  templateUrl: './add-edit-user.component.html',
  styleUrls: ['./add-edit-user.component.scss'],
})
export class AddEditUserComponent implements OnInit {
  userInfoForm!: FormGroup;
  isPersonFormValid = false;
  personInfo: any;
  userId?: number;
  @ViewChild(AlertComponent) alertComponent!: AlertComponent;

  userRepository: UserRepository = new UserRepository(this.usersService);

  constructor(
    private fb: FormBuilder,
    private formService: FormService,
    private usersService: UsersService,
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private headerItemService: HeaderItemService
  ) {}
  ngOnDestroy(): void {
    this.headerItemService.headerItemName.next('Manage Users');
  }

  ngOnInit(): void {
    this.userInfoForm = this.fb.group({
      username: ['', Validators.required],
      password: ['', [Validators.required, Validators.minLength(4)]],
      permissions: ['', [Validators.required, Validators.min(-1)]],
      isActive: [true],
    });

    this.activatedRoute.params.subscribe({
      next: (params) => {
        this.userId = params['userId'];
        this.fillClientFields();
      },
    });

    this.activatedRoute.fragment.subscribe((fragment) => {
      if (fragment === 'adding') {
        this.headerItemService.headerItemName.next('Add New User');
        this.userRepository = new UserRepository(this.usersService);
      } else {
        this.headerItemService.headerItemName.next('Update New User');
        this.userRepository.findByClientId(this.userId!).subscribe();
      }
    });
  }

  private fillClientFields() {
    if (!this.userId) {
      return;
    }
    this.userRepository.findByClientId(this.userId).subscribe((user) => {
      this.userInfoForm.patchValue(user);
      this.formService.fillPersonData.next(user.person);
    });
  }

  // You can create a method to handle form submission
  onSubmit(): void {
    if (this.userInfoForm.valid && this.isPersonFormValid) {
      this.userRepository.userDataForAddAndUpdate = {
        username: this.userInfoForm.value.username,
        password: this.userInfoForm.value.password,
        permissions: this.userInfoForm.value.permissions,
        isActive: this.userInfoForm.value.isActive,
        person: this.personInfo,
      };

      this.saveClient();
    }
  }

  private saveClient() {
    this.userRepository.save().subscribe(
      (result) => {
        this.alertComponent?.show('User saved successfully', 'success');
        this.headerItemService.headerItemName.next('Update New User');
      },
      (err) => {
        this.alertComponent?.show('Failed to save user.', 'error');
      }
    );
  }

  onReset() {
    this.formService.resetFields.next();
  }

  onCancel() {
    // this.clientsDataService.refreshClients$.next(); // raise the event to call subscribers
    this.router.navigate(['../'], { relativeTo: this.activatedRoute });
  }

  onPersonFormStatusChanged(event: { isValid: boolean; personInfo: any }) {
    this.isPersonFormValid = event.isValid;
    this.personInfo = event.personInfo;
  }
}
