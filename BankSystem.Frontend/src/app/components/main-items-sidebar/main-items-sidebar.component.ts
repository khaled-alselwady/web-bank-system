import { Component } from '@angular/core';
import type { Item } from '../../components/item-sidebar/item-model';

@Component({
  selector: 'app-main-sidebar',
  templateUrl: './main-items-sidebar.component.html',
  styleUrls: ['./main-items-sidebar.component.scss'],
})
export class MainItemsSidebarComponent {
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
  selectedIndex: number | null = 0;

  onSelectItem(index: number) {
    this.selectedIndex = index;
  }

  private getImageSrc(item: Item, index: number) {
    if (this.selectedIndex === index) {
      return item.image.src.replace('-gray', '-white');
    }
    return item.image.src;
  }

  getUpdatedItem(item: Item, index: number) {
    return {
      ...item,
      image: {
        ...item.image,
        src: this.getImageSrc(item, index),
      },
    };
  }
}
