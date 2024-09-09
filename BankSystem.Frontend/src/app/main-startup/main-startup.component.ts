import { Component, OnDestroy, OnInit } from '@angular/core';
import { LoginScreenService } from '../services/login-screen.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-main-startup',
  templateUrl: './main-startup.component.html',
  styleUrls: ['./main-startup.component.scss'],
})
export class MainStartupComponent implements OnInit, OnDestroy {
  showLoginScreen = false;
  private showLoginSubscription?: Subscription;

  constructor(private loginScreenService: LoginScreenService) {}

  ngOnInit(): void {
    this.showLoginSubscription =
      this.loginScreenService.showLoginScreen$.subscribe((show) => {
        this.showLoginScreen = show;
      });
  }

  ngOnDestroy(): void {
    this.showLoginSubscription?.unsubscribe();
  }

  onLoginScreenClose() {
    this.loginScreenService.setShowLoginScreen(false);
  }

  onLogin(data: { email: string; password: string }) {
    this.loginScreenService.setShowLoginScreen(true);
    console.log(data);
  }
}
