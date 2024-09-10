import { Component, Input, OnInit } from '@angular/core';
import { Item } from './item-model';

@Component({
  selector: 'app-item-sidebar',
  templateUrl: './item-sidebar.component.html',
  styleUrls: ['./item-sidebar.component.scss'],
})
export class ItemSidebarComponent {
  @Input() item!: Item;
}
