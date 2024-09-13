import { AfterViewChecked, Component, Input } from '@angular/core';
import { Subject } from 'rxjs';
import { take } from 'rxjs/operators';

@Component({
  selector: 'app-data-grid',
  templateUrl: './data-grid.component.html',
  styleUrls: ['./data-grid.component.scss'],
})
export class DataGridComponent implements AfterViewChecked {
  @Input() dataSource$ = new Subject<any[]>();
  @Input() displayedColumns: string[] = [];
  @Input() categoriesForFiltering: string[] = [
    'Id',
    'Full Name',
    'Email',
    'Phone Number',
  ];
  @Input() actions: {
    update: (element: any) => void;
    remove: (element: any) => void;
  } = {
    update: () => {},
    remove: () => {},
  };

  selectedCategory = this.categoriesForFiltering[0]; // Default selected category
  filterText = ''; // The entered filter text
  filteredData: any[] = []; // To hold the filtered data
  isFilterByGender = false;
  isFilterByStatus = false;
  dataSource: any[] = [];

  constructor() {}
  ngAfterViewChecked(): void {
    this.dataSource$.pipe(take(1)).subscribe((data) => {
      this.dataSource = [...data];
      this.filteredData = [...data];
    });
  }

  ngOnInit() {
    this.dataSource$.pipe(take(1)).subscribe((data) => {
      console.log(data);
      this.dataSource = [...data];
      this.filteredData = [...data];
    });
  }

  private showAllData() {
    this.filteredData = [...this.dataSource];
  }

  // Converts column name to camelCase
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

  checkGenderOrStatusSelected(selected: string) {
    this.filterText = '';
    this.showAllData();
    if (selected.toLowerCase() === 'gender') {
      this.isFilterByGender = true;
      this.isFilterByStatus = false;
    } else if (selected.toLowerCase() === 'status') {
      this.isFilterByGender = false;
      this.isFilterByStatus = true;
    } else {
      this.isFilterByGender = false;
      this.isFilterByStatus = false;
    }
  }

  applyFilterGender(value: string) {
    if (value.toLowerCase() === 'all') {
      this.showAllData();
      return;
    }

    this.filteredData = this.dataSource.filter((item) => {
      return item.gender.toLowerCase() === value.toLowerCase();
    });
  }

  applyFilterStatus(value: string) {
    if (value.toLowerCase() === 'all') {
      this.showAllData();
      return;
    }

    this.filteredData = this.dataSource.filter((item) => {
      return item.status.toLowerCase() === value.toLowerCase();
    });
  }

  // Filtering logic
  applyFilter() {
    const categoryKey = this.convertToCamelCase(this.selectedCategory);
    if (this.filterText.trim() === '') {
      this.showAllData();
    } else {
      // Filter based on the selected category and input text
      this.filteredData = this.dataSource.filter((item) => {
        const itemValue = item[categoryKey]?.toString().toLowerCase() || '';
        return itemValue.startsWith(this.filterText.toLowerCase());
      });
    }
  }
}
