import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-data-grid',
  templateUrl: './data-grid.component.html',
  styleUrls: ['./data-grid.component.scss'],
})
export class DataGridComponent {
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

  convertToCamelCase(column: string) {
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
