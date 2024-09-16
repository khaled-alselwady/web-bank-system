import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-alert',
  templateUrl: './alert.component.html',
  styleUrls: ['./alert.component.scss'],
})
export class AlertComponent {
  @Input() message: string = '';
  @Input() type: 'success' | 'error' = 'success';
  isVisible: boolean = false;

  show() {
    this.isVisible = true;
    setTimeout(() => (this.isVisible = false), 3000); // Auto-hide after 3 seconds
  }
}
