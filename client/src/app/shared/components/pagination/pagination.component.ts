import { Component, Input, OnChanges, SimpleChanges } from '@angular/core';
import { IPaginationDelegate } from '../../models/pagination-delegate';

@Component({
  selector: 'app-pagination',
  templateUrl: './pagination.component.html',
  styleUrls: ['./pagination.component.scss'],
})
export class PaginationComponent implements OnChanges {
  isPreviousEnabled: Boolean = false;
  isNextEnabled: Boolean = false;
  currentPageIndex = 1;
  @Input() pageItemCount: number;
  @Input() delegate: IPaginationDelegate | undefined;

  constructor() {
    this.pageItemCount = 1;
  }

  ngOnChanges(changes: SimpleChanges): void {
    this.updatePaginationUI();
  }

  loadProductsAtPage(pageIndex: number) {
    if (pageIndex == this.currentPageIndex) return;
    this.currentPageIndex = pageIndex;
    this.delegate?.loadDataAtPageIndex(pageIndex);
    this.updatePaginationUI();
  }

  loadPreviousProducts() {
    this.loadProductsAtPage(this.currentPageIndex - 1);
  }

  loadNextProducts() {
    this.loadProductsAtPage(this.currentPageIndex + 1);
  }

  updatePaginationUI() {
    this.isPreviousEnabled = this.currentPageIndex - 1 != 0;
    this.isNextEnabled = this.currentPageIndex + 1 <= this.pageItemCount;
  }

  counter(i: number) {
    return new Array(i);
  }
}
