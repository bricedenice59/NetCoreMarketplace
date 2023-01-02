import { Component, OnInit } from '@angular/core';
import { IPagination } from '../shared/models/pagination';
import { IProduct } from '../shared/models/product';
import { IProductType } from '../shared/models/productType';
import { ShopService } from './shop.service';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss'],
})
export class ShopComponent implements OnInit {
  pagination!: IPagination;
  products: IProduct[] = [];
  productTypes: IProductType[] = [];
  productTypeSelected: string | undefined;
  defaultSortOptionName = 'name';
  sortOptionSelected = 'name';
  sortOptions = [
    { name: 'Alphabetical', value: this.defaultSortOptionName },
    { name: 'Highest Price First', value: 'priceDesc' },
    { name: 'Lowest Price First', value: 'priceAsc' },
  ];

  allItemsName = 'All';

  isPreviousEnabled: Boolean = false;
  isNextEnabled: Boolean = false;
  currentPageIndex = 1;
  pageItemCount = 1;

  constructor(private shopService: ShopService) {}
  ngOnInit(): void {
    this.getProducts();
    this.getProductTypes();
  }

  getProducts(productTypeId?: string, sortOption?: string) {
    console.log(this.currentPageIndex);
    this.shopService
      .getProducts(
        this.productTypeSelected === this.allItemsName
          ? undefined
          : productTypeId,
        sortOption,
        this.currentPageIndex
      )
      .subscribe(
        (response: IPagination) => {
          this.pagination = response;
          this.products = this.pagination.data;
          this.pageItemCount =
            this.pagination.pageSize == 0
              ? 1
              : Math.ceil(this.pagination.count / this.pagination.pageSize);
          this.updatePaginationUI();
        },
        (error) => {
          console.error(error);
        }
      );
  }

  getProductTypes() {
    this.shopService.getProductTypes().subscribe(
      (response: any) => {
        this.productTypes = [
          { kind: this.allItemsName, id: this.allItemsName },
          ...response,
        ];
      },
      (error) => {
        console.error(error);
      }
    );
  }

  onProductTypeSelected(productTypeId: string) {
    this.productTypeSelected = productTypeId;
    this.resetPagination();
    this.getProducts(productTypeId, this.sortOptionSelected);
  }

  onSortSelected(sortOptionSelected: string) {
    this.sortOptionSelected = sortOptionSelected;
    this.getProducts(this.productTypeSelected, sortOptionSelected);
  }

  loadProductsAtPage(pageIndex: number) {
    if (pageIndex == this.currentPageIndex) return;
    this.currentPageIndex = pageIndex;
    this.getProducts(this.productTypeSelected, this.sortOptionSelected);
  }

  loadPreviousPrevious() {
    this.loadProductsAtPage(this.currentPageIndex - 1);
  }

  loadNextProducts() {
    this.loadProductsAtPage(this.currentPageIndex + 1);
  }

  updatePaginationUI() {
    this.isPreviousEnabled = this.currentPageIndex - 1 != 0;
    this.isNextEnabled = this.currentPageIndex + 1 <= this.pageItemCount;
  }

  resetPagination() {
    this.currentPageIndex = 1;
    this.pageItemCount = 1;
  }

  counter(i: number) {
    return new Array(i);
  }
}
