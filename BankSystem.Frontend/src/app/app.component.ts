import { Component } from '@angular/core';
import { UsersService } from './services/users.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent {
  constructor(private usersService: UsersService) {}
  get isLoginSuccess() {
    return this.usersService.currentUser ? true : false;
  }
}
