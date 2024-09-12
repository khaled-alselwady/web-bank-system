
import { Component, OnDestroy, OnInit } from '@angular/core';
import { ClientsService } from 'src/app/services/clients.service';

import type { ClientView } from 'src/app/models/client/client-view.model';
import { Subject, Subscription } from 'rxjs';

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

  subscriptions: Subscription[] = [];

  constructor(private clientsService: ClientsService) {}

  ngOnDestroy(): void {
    this.subscriptions.forEach((sub) => sub.unsubscribe());
  }

  private getCountClients() {
    this.subscriptions.push(
      this.clientsService.count().subscribe((count) => {
        this.clientsCount$.next(count);
      })
    );
  }

  ngOnInit(): void {
    this.subscriptions.push(
      this.clientsService.pagerUserByPageNumber(1, 10).subscribe((data) => {
        this.clientsData = data;
        this.getCountClients();
      })
    );
  }

  onChangePageNumber(event: { pageNumber: number; pageSize: number }) {
    this.subscriptions.push(
      this.clientsService
        .pagerUserByPageNumber(event.pageNumber, event.pageSize)
        .subscribe((data) => {
          this.clientsData = data;
        })
    );
  }

  updateRow(element: ClientView) {
    console.log(element);
  }

  removeRow(element: ClientView) {
    console.log(element);
  }
}
