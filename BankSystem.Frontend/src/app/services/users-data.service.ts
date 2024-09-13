import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';
import { take } from 'rxjs/operators';
import { UserView } from '../models/user/user-view.model';
import { UsersService } from './users.service';

@Injectable({ providedIn: 'root' })
export class UsersDataService {
  allDataInPage$ = new Subject<UserView[]>();
  countUsers$ = new Subject<number>();

  constructor(private usersService: UsersService) {}

  private getCountUsers() {
    this.usersService
      .count()
      .pipe(take(1))
      .subscribe((count) => {
        this.countUsers$.next(count);
      });
  }

  fetchData(pageData: { pageNumber: number; pageSize: number }) {
    this.usersService
      .pagerUsersByPageNumber(pageData.pageNumber, pageData.pageSize)
      .pipe(take(1))
      .subscribe((data) => {
        this.allDataInPage$.next(data);
        this.getCountUsers();
      });
  }
}
