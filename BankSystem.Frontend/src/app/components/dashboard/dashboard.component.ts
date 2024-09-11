import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { ClientsService } from 'src/app/services/clients.service';
import { UsersService } from 'src/app/services/users.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss'],
})
export class DashboardComponent implements OnInit, OnDestroy {
  subscriptions?: Subscription[] = [];

  constructor(
    private clientsService: ClientsService,
    private usersService: UsersService
  ) {}

  clientsCount: number = 0;
  usersCount: number = 0;

  ngOnInit(): void {
    this.subscriptions?.push(
      this.clientsService.count().subscribe((count) => {
        this.clientsCount = count;
      })
    );

    this.subscriptions?.push(
      this.usersService.count().subscribe((count) => {
        this.usersCount = count;
      })
    );
  }

  ngOnDestroy(): void {
    this.subscriptions?.forEach((subscription) => {
      subscription.unsubscribe();
    });
  }
}
