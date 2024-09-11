import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-paginator',
  templateUrl: './paginator.component.html',
  styleUrls: ['./paginator.component.scss'],
})
export class PaginatorComponent implements OnInit {
  sizes: number[] = [10, 20, 30, 40, 50];
  currentPage: number = 0;
  totalPages: number = 0;

  constructor() {}

  ngOnInit(): void {}
}
