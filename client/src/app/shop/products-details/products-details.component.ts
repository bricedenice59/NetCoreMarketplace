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
  quantity: number;

  constructor(
    private shopService: ShopService,
    private activatedRoute: ActivatedRoute,
    private basketService: BasketService
  ) {
    this.quantity = 0;
  }

  ngOnInit(): void {
    let id = this.activatedRoute.snapshot.paramMap.get('id');
    if (!id) return;

    this.loadProduct(id);
    this.basketService.basket$.subscribe((value) => {
      const item = value?.items.find((y) => y.id === id);
      if (item) {
        this.quantity = item.quantity;
      } else this.quantity = 1;
    });
  }

  loadProduct(id: string) {
    if (!id) return;
    this.shopService.getProduct(id).subscribe(
      (response) => {
        this.product = response;
      },
      (error) => {
        console.error(error);
      }
    );
  }

  addItemToBasket() {
    this.basketService.addItemToBasket(this.product, this.quantity);
  }

  incrementQuantity() {
    this.quantity += 1;
  }

  decrementQuantity() {
    if (this.quantity === 0) return;
    this.quantity -= 1;
  }
}
