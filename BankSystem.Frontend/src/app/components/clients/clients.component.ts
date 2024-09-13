import { Component, OnDestroy, OnInit } from '@angular/core';

import type { ClientView } from 'src/app/models/client/client-view.model';
import { Subject, Subscription } from 'rxjs';
import { ClientsDataService } from 'src/app/services/clients-data.service';
import { take } from 'rxjs/operators';

@Component({
  selector: 'app-clients',
  templateUrl: './clients.component.html',
  styleUrls: ['./clients.component.scss'],
})
export class ClientsComponent implements OnInit, OnDestroy {
  displayedColumnsName: string[] = [
    'Id',
    'Account Number',
    'Full Name',
    'Gender',
    'Phone',
    'Email',
    'Balance',
    'Status',
    'Actions',
  ];
  clientsData$ = new Subject<ClientView[]>();
  clientsCount$ = new Subject<number>();
  catagoriesForFiltering = [
    'Id',
    'Account Number',
    'Full Name',
    'Gender',
    'Phone',
    'Email',
    'Status',
  ];

  subscriptions: Subscription[] = [];

  constructor(private clientsDataService: ClientsDataService) {}

  private initialDataSubscription(
    event: { pageNumber: number; pageSize: number } = {
      pageNumber: 1,
      pageSize: 10,
    }
  ) {
    this.clientsDataService.fetchData(event);

    const dataSub = this.clientsDataService.allDataInPage$
      .pipe(take(1))
      .subscribe((data) => {
        this.clientsData$.next(data);
      });
    const countSub = this.clientsDataService.countClients$
      .pipe(take(1))
      .subscribe((count) => {
        this.clientsCount$.next(count);
      });

    this.subscriptions.push(dataSub, countSub);
  }

  ngOnDestroy(): void {
    this.subscriptions.forEach((sub) => sub.unsubscribe());
  }

  ngOnInit(): void {
    this.initialDataSubscription();
  }

  updateRow(element: ClientView) {
    console.log(element);
  }

  removeRow(element: ClientView) {
    console.log(element);
  }

  onChangeDataByPaginator(event: { pageNumber: number; pageSize: number }) {
    this.initialDataSubscription(event);
  }
}
