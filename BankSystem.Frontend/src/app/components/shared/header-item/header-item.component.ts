import { Component, Input } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-header-item',
  templateUrl: './header-item.component.html',
  styleUrls: ['./header-item.component.scss'],
})
export class HeaderItemComponent {
  @Input() titleName: string = 'Title';
  @Input() buttonName: string | undefined = undefined;
  @Input() routerLink: string | any[] | null | undefined = undefined;

  constructor(private router: Router, private activatedRoute: ActivatedRoute) {}

  onClick() {
    this.router.navigate(['new'], {
      relativeTo: this.activatedRoute,
      fragment: 'adding',
    });
  }
}
