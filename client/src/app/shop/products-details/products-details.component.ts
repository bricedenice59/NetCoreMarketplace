import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { BasketService } from 'src/app/basket/basket.service';
import { IProduct } from 'src/app/shared/models/product';
import { ShopService } from 'src/app/shop/shop.service';

@Component({
  selector: 'app-products-details',
  templateUrl: './products-details.component.html',
  styleUrls: ['./products-details.component.scss'],
})
export class ProductsDetailsComponent implements OnInit {
  product!: IProduct;

  constructor(
    private shopService: ShopService,
    private activatedRoute: ActivatedRoute,
    private basketService: BasketService
  ) {}

  ngOnInit(): void {
    this.loadProduct();
  }

  loadProduct() {
    let param = this.activatedRoute.snapshot.paramMap.get('id');
    if (!param) return;
    this.shopService.getProduct(param).subscribe(
      (response) => {
        this.product = response;
      },
      (error) => {
        console.error(error);
      }
    );
  }

  addItemToBasket() {
    this.basketService.addItemToBasket(this.product);
  }
}
