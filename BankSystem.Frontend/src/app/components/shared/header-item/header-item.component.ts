import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-header-item',
  templateUrl: './header-item.component.html',
  styleUrls: ['./header-item.component.scss'],
})
export class HeaderItemComponent implements OnInit, OnDestroy {
  @Input() titleName: string = 'Title';
  @Input() buttonName: string | undefined = undefined;
  @Input() routerLink: string | any[] | null | undefined = undefined;
  isAddingMode = false;
  private fragmentSub?: Subscription;

  constructor(private router: Router, private activatedRoute: ActivatedRoute) {}

  ngOnInit(): void {
    this.fragmentSub = this.activatedRoute.fragment.subscribe(
      (data) => (this.isAddingMode = !!data)
    );
  }

  ngOnDestroy(): void {
    this.fragmentSub?.unsubscribe();
  }

  onClick() {
    this.router.navigate(['new'], {
      relativeTo: this.activatedRoute,
      fragment: 'adding',
    });
  }
}
