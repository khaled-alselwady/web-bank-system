import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-filter',
  templateUrl: './filter.component.html',
  styleUrls: ['./filter.component.scss'],
})
export class FilterComponent {
  @Input() categories: string[] = ['Id', 'Full Name', 'Email', 'Phone Number'];
  enteredText = '';

  isFilterByGender = false;
  isFilterByStatus = false;

  onSelectCategory(event: Event) {
    const selectedCategory = (
      event.target as HTMLSelectElement
    ).value.toLowerCase();

    if (selectedCategory === 'gender') {
      this.isFilterByGender = true;
      this.isFilterByStatus = false;
    } else if (selectedCategory === 'status') {
      this.isFilterByGender = false;
      this.isFilterByStatus = true;
    } else {
      this.isFilterByGender = false;
      this.isFilterByStatus = false;
    }
  }
}
