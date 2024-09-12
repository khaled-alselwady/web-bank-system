
import {
  Component,
  EventEmitter,
  Input,
  OnDestroy,
  OnInit,
  Output,
} from '@angular/core';
import { Subject, Subscription } from 'rxjs';

@Component({
  selector: 'app-paginator',
  templateUrl: './paginator.component.html',
  styleUrls: ['./paginator.component.scss'],
})
export class PaginatorComponent implements OnInit, OnDestroy {
  @Input() sizes: number[] = [10, 20, 30, 40, 50];
  @Input() totalRecords$?: Subject<number>;
  @Output() change = new EventEmitter<{
    pageNumber: number;
    pageSize: number;
  }>();
  totalRecords: number = 10;
  currentPage: number = 0;
  totalPages: number = 0;
  currentSize: number = 10;

  private subscription?: Subscription;

  constructor() {}

  ngOnInit(): void {
    this.subscription = this.totalRecords$?.subscribe((data) => {
      this.totalRecords = data;
      this.sortSizes();
      this.totalPages = this.calculateTotalPages(this.sizes[0]);
      this.currentPage = this.totalPages ? 1 : 0;
    });
  }

  ngOnDestroy(): void {
    this.subscription?.unsubscribe();
  }

  onCurrentSizeChanged(event: Event) {
    const selectedOption = event.target as HTMLSelectElement;
    this.currentSize = +selectedOption.value;
    this.totalPages = this.calculateTotalPages(this.currentSize);

    this.change.emit({
      pageNumber: this.currentPage,
      pageSize: this.currentSize,
    });
  }

  onCurrentPageChanged(newCurrentPageNumber: number) {
    if (newCurrentPageNumber < 1 || newCurrentPageNumber > this.totalPages) {
      return;
    }

    this.currentPage = newCurrentPageNumber;
    this.change.emit({
      pageNumber: this.currentPage,
      pageSize: this.currentSize,
    });
  }

  calculateTotalPages(currentSize: number) {
    return Math.ceil(this.totalRecords / currentSize);
  }

  private sortSizes() {
    this.sizes = this.sizes.sort((a, b) => {
      return a > b ? 1 : -1;
    });
  }
}
