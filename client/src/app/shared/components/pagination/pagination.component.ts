import {
  Component,
  EventEmitter,
  Input,
  OnChanges,
  Output,
  SimpleChanges,
} from '@angular/core';

@Component({
  selector: 'app-pagination',
  templateUrl: './pagination.component.html',
  styleUrls: ['./pagination.component.scss'],
})
export class PaginationComponent implements OnChanges {
  isPreviousEnabled: Boolean = false;
  isNextEnabled: Boolean = false;

  @Input() pageItemCount: number;
  @Input() currentPageIndex: number;
  @Output() pageChanged: EventEmitter<number>;

  constructor() {
    this.pageItemCount = 1;
    this.currentPageIndex = 1;
    this.pageChanged = new EventEmitter();
  }

  ngOnChanges(changes: SimpleChanges): void {
    this.updatePaginationUI();
  }

  onPageChanged(pageIndex: number) {
    this.pageChanged.emit(pageIndex);
  }

  loadProductsAtPage(pageIndex: number) {
    if (pageIndex == this.currentPageIndex) return;
    this.currentPageIndex = pageIndex;
    this.onPageChanged(pageIndex);
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
