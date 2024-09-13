import { Component, OnDestroy, OnInit } from '@angular/core';
import { UsersService } from 'src/app/services/users.service';
import type { UserView } from 'src/app/models/user/user-view.model';
import { Subject, Subscription } from 'rxjs';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.scss'],
})
export class UsersComponent implements OnInit, OnDestroy {
  displayedColumnsName: string[] = [
    'Id',
    'Username',
    'Full Name',
    'Gender',
    'Phone',
    'Email',
    'Status',
    'Actions',
  ];

  usersData: UserView[] = [];
  usersData$ = new Subject<UserView[]>();
  usersCount$ = new Subject<number>();

  subscriptions: Subscription[] = [];

  constructor(private usersService: UsersService) {}

  ngOnInit(): void {
    this.subscriptions.push(
      this.usersService.pagerUsersByPageNumber(1, 10).subscribe((data) => {
        this.usersData = data;
        this.getCountUsers();
      })
    );
  }

  ngOnDestroy(): void {
    this.subscriptions.forEach((sub) => sub.unsubscribe());
  }

  private getCountUsers() {
    this.subscriptions.push(
      this.usersService.count().subscribe((count) => {
        this.usersCount$.next(count);
      })
    );
  }

  onChangePageNumber(event: { pageNumber: number; pageSize: number }) {
    this.subscriptions.push(
      this.usersService
        .pagerUsersByPageNumber(event.pageNumber, event.pageSize)
        .subscribe((data) => {
          this.usersData = data;
        })
    );
  }

  updateRow(element: UserView) {
    console.log(element);
  }

  removeRow(element: UserView) {
    console.log(element);
  }
}
