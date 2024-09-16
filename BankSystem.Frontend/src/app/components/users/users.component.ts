import { Component, OnDestroy, OnInit } from '@angular/core';
import type { UserView } from 'src/app/models/user/user-view.model';
import { Subject, Subscription } from 'rxjs';
import { UsersDataService } from 'src/app/services/users-data.service';
import { take } from 'rxjs/operators';
import { ActivatedRoute, Router } from '@angular/router';

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
  actions = {
    update: (element: UserView) => this.updateRow(element),
    remove: (element: UserView) => this.removeRow(element),
  };

  usersData: UserView[] = [];
  usersData$ = new Subject<UserView[]>();
  usersCount$ = new Subject<number>();

  subscriptions: Subscription[] = [];
  isAddingMode = false;

  constructor(
    private usersServiceData: UsersDataService,
    private router: Router,
    private activatedRoute: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.initialDataSubscription();

    this.subscriptions.push(
      this.activatedRoute.fragment.subscribe((fragment) => {
        this.isAddingMode = !!fragment;
      })
    );

    // this.subscriptions.push(
    //   this.usersServiceData.refreshClients$.subscribe(() => {
    //     this.initialDataSubscription();
    //   })
    // );
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
    this.router.navigate([element.id], {
      relativeTo: this.activatedRoute,
      fragment: 'editing',
    });
  }

  removeRow(element: UserView) {
    console.log(element);
  }
}
