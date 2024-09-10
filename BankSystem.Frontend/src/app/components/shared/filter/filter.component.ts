import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-filter',
  templateUrl: './filter.component.html',
  styleUrls: ['./filter.component.scss'],
})
export class FilterComponent {
  @Input() categories: string[] = ['Id', 'Full Name', 'Email', 'Phone Number'];
  enteredText = '';
}
