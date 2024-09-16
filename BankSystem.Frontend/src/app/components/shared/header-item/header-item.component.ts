import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { HeaderItemService } from 'src/app/services/header-item.service';

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
  private subscriptions: Subscription[] = [];

  constructor(
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private headerItemService: HeaderItemService
  ) {}

  ngOnInit(): void {
    this.subscriptions.push(
      this.activatedRoute.fragment.subscribe(
        (data) => (this.isAddingMode = !!data)
      )
    );

    this.subscriptions.push(
      this.headerItemService.headerItemName.subscribe((headerName) => {
        this.titleName = headerName;
      })
    );
  }

  ngOnDestroy(): void {
    this.subscriptions.forEach((sub) => sub.unsubscribe());
  }

  onClick() {
    this.router.navigate(['new'], {
      relativeTo: this.activatedRoute,
      fragment: 'adding',
    });
  }
}
