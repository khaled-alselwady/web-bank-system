import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-header-item',
  templateUrl: './header-item.component.html',
  styleUrls: ['./header-item.component.scss'],
})
export class HeaderItemComponent {
  @Input() titleName: string = 'Title';
  @Input() buttonName: string | undefined = undefined;
}
