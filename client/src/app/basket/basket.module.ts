import { EventEmitter, Input, NgModule, OnInit, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BasketComponent } from './basket.component';
import { BasketRoutingModule } from './basket-routing.module';
import { BasketSummaryComponent } from '../shared/components/basket-summary/basket-summary.component';
import { IBasketItem } from '../shared/models/basket';

@NgModule({
  declarations: [BasketComponent, BasketSummaryComponent],
  imports: [CommonModule, BasketRoutingModule],
})
export class BasketModule implements OnInit {
  @Output() decrement: EventEmitter<IBasketItem> =
    new EventEmitter<IBasketItem>();
  @Output() increment: EventEmitter<IBasketItem> =
    new EventEmitter<IBasketItem>();
  @Output() remove: EventEmitter<IBasketItem> = new EventEmitter<IBasketItem>();
  @Input() isBasket = true;
  @Input() items: any;
  @Input() isOrder = false;

  constructor() {}

  ngOnInit(): void {}

  decrementItemQuantity(item: IBasketItem) {
    this.decrement.emit(item);
  }

  incrementItemQuantity(item: IBasketItem) {
    this.increment.emit(item);
  }

  removeBasketItem(item: IBasketItem) {
    this.remove.emit(item);
  }
}
