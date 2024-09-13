import { Component, OnDestroy, OnInit } from '@angular/core';
import type { UserView } from 'src/app/models/user/user-view.model';
import { Subject, Subscription } from 'rxjs';
import { UsersDataService } from 'src/app/services/users-data.service';
import { take } from 'rxjs/operators';

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
  catagoriesForFiltering = [
    'Id',
    'Username',
    'Full Name',
    'Gender',
    'Phone',
    'Email',
    'Status',
  ];

  usersData: UserView[] = [];
  usersData$ = new Subject<UserView[]>();
  usersCount$ = new Subject<number>();

  subscriptions: Subscription[] = [];

  constructor(private usersServiceData: UsersDataService) {}

  ngOnInit(): void {
    this.initialDataSubscription();
  }

  ngOnDestroy(): void {
    this.subscriptions.forEach((sub) => sub.unsubscribe());
  }

  private initialDataSubscription(
    event: { pageNumber: number; pageSize: number } = {
      pageNumber: 1,
      pageSize: 10,
    }
  ) {
    this.usersServiceData.fetchData(event);

    const dataSub = this.usersServiceData.allDataInPage$
      .pipe(take(1))
      .subscribe((data) => {
        this.usersData$.next(data);
      });
    const countSub = this.usersServiceData.countUsers$
      .pipe(take(1))
      .subscribe((count) => {
        this.usersCount$.next(count);
      });

    this.subscriptions.push(dataSub, countSub);
  }

  onChangeDataByPaginator(event: { pageNumber: number; pageSize: number }) {
    this.initialDataSubscription(event);
  }

  updateRow(element: UserView) {
    console.log(element);
  }

  removeRow(element: UserView) {
    console.log(element);
  }
}
