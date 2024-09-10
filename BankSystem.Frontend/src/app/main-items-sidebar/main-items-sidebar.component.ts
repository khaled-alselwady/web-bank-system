import { Component, OnInit } from '@angular/core';
import { Item } from '../item-sidebar/item-model';

@Component({
  selector: 'app-main-sidebar',
  templateUrl: './main-items-sidebar.component.html',
  styleUrls: ['./main-items-sidebar.component.scss'],
})
export class MainItemsSidebarComponent implements OnInit {
  items: Item[] = [
    {
      image: {
        src: '../../assets/icons/dashboard-gray-sidebar.png',
        alt: 'dashboard',
      },
      title: 'Dashboard',
    },

    {
      image: {
        src: '../../assets/icons/clients-gray-sidebar.png',
        alt: 'clients',
      },
      title: 'Clients',
    },

    {
      image: {
        src: '../../assets/icons/transactions-gray-sidebar.png',
        alt: 'transactions',
      },
      title: 'Transactions',
    },

    {
      image: {
        src: '../../assets/icons/login-registers-gray-sidebar.png',
        alt: 'login-registers',
      },
      title: 'Login Registers',
    },

    {
      image: {
        src: '../../assets/icons/users-gray-sidebar.png',
        alt: 'users',
      },
      title: 'Users',
    },
  ];
  constructor() {}

  ngOnInit(): void {}
}
