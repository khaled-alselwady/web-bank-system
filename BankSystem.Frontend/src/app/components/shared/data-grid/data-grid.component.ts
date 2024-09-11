import { Component, Input, OnInit } from '@angular/core';
import { ClientView } from 'src/app/models/client/client-view.model';
import { ClientsService } from 'src/app/services/clients.service';

@Component({
  selector: 'app-data-grid',
  templateUrl: './data-grid.component.html',
  styleUrls: ['./data-grid.component.scss'],
})
export class DataGridComponent implements OnInit {
removeRow(_t99: any) {
throw new Error('Method not implemented.');
}
updateRow(_t99: any) {
throw new Error('Method not implemented.');
}
  displayedColumns: string[] = [
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
  @Input() dataSource: any[] = [];

  clientsData: ClientView[] = [];

  constructor(private clientsService: ClientsService) {}

  ngOnInit() {
    this.clientsService.fetchAll().subscribe((data) => {
      console.log(data);
      this.clientsData = data;
    });
  }
}
