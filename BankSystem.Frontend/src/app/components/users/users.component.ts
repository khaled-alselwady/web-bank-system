import { Component, OnInit } from '@angular/core';
import { UsersService } from 'src/app/services/users.service';
import type { UserView } from 'src/app/models/user/user-view.model';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.scss'],
})
export class UsersComponent implements OnInit {
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

  usersData: UserView[] = [];

  constructor(private usersService: UsersService) {}

  ngOnInit(): void {
    this.usersService.fetchAll().subscribe((data) => {
      this.usersData = data;
    });
  }

  updateRow(element: UserView) {
    console.log(element);
  }

  removeRow(element: UserView) {
    console.log(element);
  }
}
