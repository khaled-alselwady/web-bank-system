import { Component, OnDestroy } from '@angular/core';
import { NgForm } from '@angular/forms';
import { LoginScreenService } from '../../services/login-screen.service';
import { UsersService } from '../../services/users.service';
import { Subscription } from 'rxjs/internal/Subscription';
import { Router } from '@angular/router';

@Component({
  selector: 'app-auth',
  templateUrl: './auth.component.html',
  styleUrls: ['./auth.component.scss'],
})
export class AuthComponent implements OnDestroy {
  errorMessage: string = '';
  private userServiceSubscriptions: Subscription[] = [];

  constructor(
    private loginScreenService: LoginScreenService,
    private usersService: UsersService,
    private router: Router
  ) {}

  private checkUserInput(formData: NgForm) {
    if (formData.form.controls.username.invalid) {
      this.errorMessage = 'username is required.';
      return false;
    }

    if (formData.form.controls.password.invalid) {
      this.errorMessage = 'Please enter a valid password.';
      return false;
    }

    if (formData.invalid) {
      this.errorMessage = 'Please fill out all required fields.';
      return false;
    }

    return true;
  }

  private getUserInfo(formData: NgForm) {
    return this.usersService
      .findUserByUsernameAndPassword(
        formData.value.username,
        formData.value.password
      )
      .subscribe({
        next: (user) => {
          if (user) {
            if (!user.isActive) {
              this.errorMessage = 'User is not active.';
              return;
            }
            this.usersService.currentUser = user;
            this.loginScreenService.setShowLoginScreen(false);
            formData.reset();
            this.router.navigate(['/dashboard']);
          } else {
            this.errorMessage = 'Invalid email or password.';
          }
        },
        error: (error) => {
          this.errorMessage = error;
        },
      });
  }

  private checkExistsUser(formData: NgForm) {
    return this.usersService
      .existsUserByUsernameAndPassword(
        formData.value.username,
        formData.value.password
      )
      .subscribe({
        next: (exists) => {
          if (exists) {
            this.userServiceSubscriptions?.push(this.getUserInfo(formData));
          } else {
            this.errorMessage = 'Invalid email or password.';
          }
        },
        error: (error) => {
          console.log(error);
          this.errorMessage = error;
        },
      });
  }

  private resetErrorMessage() {
    this.errorMessage = '';
  }

  onSubmit(formData: NgForm) {
    if (!this.checkUserInput(formData)) {
      return;
    }
    this.resetErrorMessage();

    this.userServiceSubscriptions.push(this.checkExistsUser(formData));
  }

  onClose() {
    this.loginScreenService.setShowLoginScreen(false);
  }

  ngOnDestroy(): void {
    if (!this.userServiceSubscriptions) {
      return;
    }

    this.userServiceSubscriptions.forEach((subscription) => {
      subscription.unsubscribe();
    });
  }
}
