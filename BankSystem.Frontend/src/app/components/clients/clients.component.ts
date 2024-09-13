import { Component, OnDestroy, OnInit } from '@angular/core';

import type { ClientView } from 'src/app/models/client/client-view.model';
import { Subject, Subscription } from 'rxjs';
import { ClientsDataService } from 'src/app/services/clients-data.service';

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
  clientsData: ClientView[] = [];
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

  ngOnDestroy(): void {
    this.subscriptions.forEach((sub) => sub.unsubscribe());
  }

  ngOnInit(): void {
    this.clientsDataService.fetchData({ pageNumber: 1, pageSize: 10 });

    this.clientsDataService.allDataInPage$.subscribe((data) => {
      this.clientsData = data;
    });
    this.clientsDataService.countClients$.subscribe((count) => {
      this.clientsCount$.next(count);
    });
  }

  updateRow(element: ClientView) {
    console.log(element);
  }

  removeRow(element: ClientView) {
    console.log(element);
  }
}
