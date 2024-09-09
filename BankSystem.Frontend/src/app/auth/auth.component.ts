import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { NgForm } from '@angular/forms';
import { LoginScreenService } from './login-screen.service';

@Component({
  selector: 'app-auth',
  templateUrl: './auth.component.html',
  styleUrls: ['./auth.component.scss'],
})
export class AuthComponent implements OnInit {
  @Output() login = new EventEmitter<{ email: string; password: string }>();
  errorMessage: string = '';

  constructor(private loginScreenService: LoginScreenService) {}
  ngOnInit(): void {}

  private checkUserInput(formData: NgForm) {
    if (formData.form.controls.email.invalid) {
      this.errorMessage = 'Please enter a valid email address.';
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

  onSubmit(formData: NgForm) {
    if (!this.checkUserInput(formData)) {
      return;
    }
    this.errorMessage = '';
    this.login.emit({
      email: formData.value.email,
      password: formData.value.password,
    });
    formData.reset();
  }

  onClose() {
    this.loginScreenService.setShowLoginScreen(false);
  }
}
