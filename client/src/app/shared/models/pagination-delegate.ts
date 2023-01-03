import { Observable } from 'rxjs';

export interface IPaginationDelegate {
  loadDataAtPageIndex(pageIndex: number): void;
}
