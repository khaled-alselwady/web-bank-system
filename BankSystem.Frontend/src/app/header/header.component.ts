import { Component } from '@angular/core';
import { LoginScreenService } from '../services/login-screen.service';
import { UsersService } from '../services/users.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss'],
})
export class HeaderComponent {
  isDropdownOpen = false;
  constructor(
    private loginScreenService: LoginScreenService,
    private usersService: UsersService
  ) {}

  get isLoginSuccess() {
    return this.usersService.currentUser ? true : false;
  }

  get isUserMale() {
    return (
      this.usersService.currentUser?.person.gender.toLowerCase() === 'male'
    );
  }

  onLogin() {
    this.loginScreenService.setShowLoginScreen(true);
  }

  onSignOut() {
    this.usersService.signOut();
    this.isDropdownOpen = false;
  }

  toggleDropdown() {
    this.isDropdownOpen = !this.isDropdownOpen;
  }
}
