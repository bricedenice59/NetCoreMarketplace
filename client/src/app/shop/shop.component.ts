import { Component, OnInit } from '@angular/core';
import { IProduct } from '../shared/models/product';
import { IProductType } from '../shared/models/productType';
import { ShopService } from './shop.service';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss'],
})
export class ShopComponent implements OnInit {
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

  constructor(private shopService: ShopService) {}
  ngOnInit(): void {
    this.getProducts();
    this.getProductTypes();
  }

  getProducts(productTypeId?: string, sortOption?: string) {
    this.shopService
      .getProducts(
        this.productTypeSelected === this.allItemsName
          ? undefined
          : productTypeId,
        sortOption
      )
      .subscribe(
        (response: any) => {
          this.products = response.data;
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
    this.getProducts(productTypeId, this.sortOptionSelected);
  }

  onSortSelected(sortOptionSelected: string) {
    this.sortOptionSelected = sortOptionSelected;
    this.getProducts(this.productTypeSelected, sortOptionSelected);
  }
}
