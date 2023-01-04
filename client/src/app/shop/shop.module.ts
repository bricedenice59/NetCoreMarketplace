import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ShopComponent } from './shop.component';
import { ProductItemComponent } from './product-item/product-item.component';
import { PaginationComponent } from '../shared/components/pagination/pagination.component';
import { ShopPagingHeaderComponent } from './shop-paging-header/shop-paging-header.component';

@NgModule({
  declarations: [ShopComponent, ProductItemComponent, PaginationComponent, ShopPagingHeaderComponent],
  imports: [CommonModule],
  exports: [ShopComponent],
})
export class ShopModule {}
