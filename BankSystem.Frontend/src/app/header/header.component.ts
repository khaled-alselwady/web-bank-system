import { Component } from '@angular/core';
import { LoginScreenService } from '../services/login-screen.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss'],
})
export class HeaderComponent {
  constructor(private loginScreenService: LoginScreenService) {}

  onLogin() {
    this.loginScreenService.setShowLoginScreen(true);
  }
}
