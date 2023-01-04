import { Component, OnInit } from '@angular/core';
import { IPagination } from '../shared/models/pagination';
import { IPaginationDelegate } from '../shared/models/pagination-delegate';
import { IProduct } from '../shared/models/product';
import { IProductType } from '../shared/models/productType';
import { ShopParams } from '../shared/models/shopParams';
import { ShopService } from './shop.service';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss'],
})
export class ShopComponent implements OnInit, IPaginationDelegate {
  pagination: IPagination | undefined;
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

  currentPageIndex = 1;
  pageItemCount = 1;

  shopParams = new ShopParams();

  constructor(private shopService: ShopService) {}

  loadDataAtPageIndex(pageIndex: number): void {
    this.currentPageIndex = pageIndex;
    this.getProducts();
  }

  ngOnInit(): void {
    this.getProducts();
    this.getProductTypes();
  }

  getProducts() {
    this.feedShopParams();
    this.shopService.getProducts(this.shopParams).subscribe(
      (response: IPagination) => {
        this.pagination = response;
        this.products = this.pagination.data;
        this.pageItemCount =
          this.pagination.pageSize == 0
            ? 1
            : Math.ceil(this.pagination.count / this.pagination.pageSize);
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
    this.currentPageIndex = 1;
    this.pageItemCount = 1;
    this.getProducts();
  }

  onSortSelected(sortOptionSelected: string) {
    this.sortOptionSelected = sortOptionSelected;
    this.getProducts();
  }

  feedShopParams() {
    if (this.shopParams.productTypeId === this.allItemsName) {
      this.shopParams.productTypeId = undefined;
    } else {
      this.shopParams.productTypeId = this.productTypeSelected;
    }

    this.shopParams.sortOption = this.sortOptionSelected;
    this.shopParams.pageIndex = this.currentPageIndex;
  }
}
