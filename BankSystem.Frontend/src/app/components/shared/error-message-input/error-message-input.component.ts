import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-error-message-input',
  templateUrl: './error-message-input.component.html',
  styleUrls: ['./error-message-input.component.scss'],
})
export class ErrorMessageInputComponent {
  @Input() errorMessage: string = '';
}
