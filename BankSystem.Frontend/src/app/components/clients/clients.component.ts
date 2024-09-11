import { Component, OnInit } from '@angular/core';
import { ClientsService } from 'src/app/services/clients.service';

import type { ClientView } from 'src/app/models/client/client-view.model';

@Component({
  selector: 'app-clients',
  templateUrl: './clients.component.html',
  styleUrls: ['./clients.component.scss'],
})
export class ClientsComponent implements OnInit {
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

  constructor(private clientsService: ClientsService) {}

  ngOnInit(): void {
    this.clientsService.fetchAll().subscribe((data) => {
      this.clientsData = data;
    });
  }

  updateRow(element: ClientView) {
    console.log(element);
  }

  removeRow(element: ClientView) {
    console.log(element);
  }
}
