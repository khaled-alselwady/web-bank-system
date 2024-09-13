import { Injectable, OnDestroy } from '@angular/core';
import { ClientView } from '../models/client/client-view.model';
import { ClientsService } from './clients.service';
import { Subject, Subscription } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class ClientsDataService implements OnDestroy {
  allDataInPage$ = new Subject<ClientView[]>();
  countClients$ = new Subject<number>();

  subscriptions: Subscription[] = [];

  constructor(private clientsService: ClientsService) {}
  ngOnDestroy(): void {
    this.subscriptions.forEach((sub) => sub.unsubscribe());
  }

  private getCountClients() {
    this.subscriptions.push(
      this.clientsService.count().subscribe((count) => {
        this.countClients$.next(count);
      })
    );
  }

  fetchData(pageData: { pageNumber: number; pageSize: number }) {
    this.subscriptions.push(
      this.clientsService
        .pagerClientsByPageNumber(pageData.pageNumber, pageData.pageSize)
        .subscribe((data) => {
          this.allDataInPage$.next(data);
          this.getCountClients();
        })
    );
  }
}
