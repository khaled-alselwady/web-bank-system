import { Injectable } from '@angular/core';
import { ClientView } from '../models/client/client-view.model';
import { ClientsService } from './clients.service';
import { Subject } from 'rxjs';
import { take } from 'rxjs/operators';

@Injectable({ providedIn: 'root' })
export class ClientsDataService {
  allDataInPage$ = new Subject<ClientView[]>();
  countClients$ = new Subject<number>();
  refreshClients$ = new Subject<void>();

  constructor(private clientsService: ClientsService) {}

  private getCountClients() {
    this.clientsService
      .count()
      .pipe(take(1))
      .subscribe((count) => {
        this.countClients$.next(count);
      });
  }

  fetchData(pageData: { pageNumber: number; pageSize: number }) {
    this.clientsService
      .pagerClientsByPageNumber(pageData.pageNumber, pageData.pageSize)
      .pipe(take(1))
      .subscribe((data) => {
        this.allDataInPage$.next(data);
        this.getCountClients();
      });
  }

  delete(id: number) {
    this.clientsService.delete(id).subscribe();
  }
}
