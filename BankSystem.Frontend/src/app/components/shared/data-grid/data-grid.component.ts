// src/app/data-grid.component.ts
import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-data-grid',
  templateUrl: './data-grid.component.html',
  styleUrls: ['./data-grid.component.scss'],
})
export class DataGridComponent implements OnInit {
  @Input() displayedColumns: string[] = [];
  @Input() dataSource: any[] = [];

  @Input() actions: {
    update: (element: any) => void;
    remove: (element: any) => void;
  } = {
    update: () => {},
    remove: () => {},
  };

  constructor() {}

  ngOnInit() {
    // console.log('Displayed Columns:', this.displayedColumns);
    // console.log('Data Source:', this.dataSource);
  }

  convertToCamelCase(column: string) {
    // we should convert the column to camel case because element['name'] => the name has to be camel case
    return column
      .split(' ')
      .map((word, index) => {
        return index == 0
          ? word.toLowerCase()
          : word.charAt(0).toUpperCase() + word.slice(1).toLowerCase();
      })
      .join('');
  }
}
