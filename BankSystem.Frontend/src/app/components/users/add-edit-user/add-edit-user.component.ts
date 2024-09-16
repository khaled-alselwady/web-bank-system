import { Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AlertComponent } from '../../shared/alert/alert.component';
import { UserRepository } from 'src/app/repositories/user-repository';
import { FormService } from 'src/app/services/form.service';
import { UsersService } from 'src/app/services/users.service';
import { ActivatedRoute, Router } from '@angular/router';
import { HeaderItemService } from 'src/app/services/header-item.service';
import { PermissionsService } from 'src/app/services/permissions.service';
import { UserPermissions } from 'src/app/enums/user-permissions.enum';

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
  permissionsOptions: string[] = [];

  constructor(
    private fb: FormBuilder,
    private formService: FormService,
    private usersService: UsersService,
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private headerItemService: HeaderItemService,
    private permissionsService: PermissionsService
  ) {}
  ngOnDestroy(): void {
    this.headerItemService.headerItemName.next('Manage Users');
  }

  ngOnInit(): void {
    this.userInfoForm = this.fb.group({
      username: ['', Validators.required],
      password: ['', [Validators.required, Validators.minLength(4)]],
      permissions: this.fb.group({
        showClients: [false],
        addNewClient: [false],
        updateClient: [false],
        removeClient: [false],
        showUsers: [false],
        addNewUser: [false],
        updateUser: [false],
        removeUser: [false],
        showTransactions: [false],
        showLoginRegisters: [false],
      }),
      isActive: [true],
    });

    this.fillPermissionsArray();

    this.activatedRoute.params.subscribe({
      next: (params) => {
        this.userId = params['userId'];
        this.fillUserFields();
      },
    });

    this.activatedRoute.fragment.subscribe((fragment) => {
      if (fragment === 'adding') {
        this.headerItemService.headerItemName.next('Add New User');
        this.userRepository = new UserRepository(this.usersService);
      } else {
        this.headerItemService.headerItemName.next('Update New User');
        this.userRepository.findByClientId(this.userId!).subscribe((user) => {
          this.selectCorrectPermissions(user.permissions);
        });
      }
    });
  }

  private fillPermissionsArray() {
    // this.permissionsOptions = [
    //   'showClients',
    //   'addNewClient',
    //   'updateClient',
    //   'removeClient',
    //   'showUsers',
    //   'addNewUser',
    //   'updateUser',
    //   'removeUser',
    //   'showTransactions',
    //   'showLoginRegisters',
    // ];
    const permissions = this.userInfoForm.controls['permissions'] as FormGroup;

    Object.keys(permissions.controls)?.forEach((key) => {
      this.permissionsOptions.push(key);
    });
  }

  private fillUserFields() {
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

      this.saveUser();
    }
  }

  private saveUser() {
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
    this.usersService.refreshClients$.next(); // raise the event to call subscribers
    this.router.navigate(['../'], { relativeTo: this.activatedRoute });
  }

  onPersonFormStatusChanged(event: { isValid: boolean; personInfo: any }) {
    this.isPersonFormValid = event.isValid;
    this.personInfo = event.personInfo;
  }

  onParentCheckboxChange(event: Event, groupName: string) {
    const target = event.target as HTMLInputElement;
    const isChecked = target.checked;

    // if the `show` entity is selected, so I don't want to do anything, but if it is unselected, then I want to unselect all its children
    if (isChecked) {
      return;
    }

    // Update child checkboxes based on parent checkbox status
    const controls = this.userInfoForm.controls['permissions'] as FormGroup;

    if (groupName === 'clients') {
      ['addNewClient', 'updateClient', 'removeClient'].forEach((id) => {
        controls.get(id)?.setValue(isChecked);
      });
    } else if (groupName === 'users') {
      ['addNewUser', 'updateUser', 'removeUser'].forEach((id) => {
        controls.get(id)?.setValue(isChecked);
      });
    }
  }

  private getPermissionsItemValue(permissionItemName: string) {
    switch (permissionItemName) {
      case 'showClients':
        return UserPermissions.SHOW_CLIENTS;
      case 'addNewClient':
        return UserPermissions.ADD_NEW_CLIENT;
      case 'updateClient':
        return UserPermissions.UPDATE_CLIENT;
      case 'removeClient':
        return UserPermissions.REMOVE_CLIENT;
      case 'showUsers':
        return UserPermissions.SHOW_USERS;
      case 'addNewUser':
        return UserPermissions.ADD_NEW_USER;
      case 'updateUser':
        return UserPermissions.UPDATE_USER;
      case 'removeUser':
        return UserPermissions.REMOVE_USER;
      case 'showTransactions':
        return UserPermissions.SHOW_TRANSACTIONS;
      case 'showLoginRegisters':
        return UserPermissions.SHOW_LOGIN_REGISTERS;
      default:
        return 0;
    }
  }

  selectCorrectPermissions(permissionsUser: number) {
    const permissions = this.userInfoForm.controls['permissions'] as FormGroup;

    this.permissionsOptions.forEach((option) => {
      if (permissionsUser === -1) {
        permissions.get(option)?.setValue(true);
      } else {
        const permissionItem = +this.getPermissionsItemValue(option);
        if (
          this.permissionsService.canAccess(permissionItem, permissionsUser)
        ) {
          permissions.get(option)?.setValue(true);
        }
      }
    });
  }
}
