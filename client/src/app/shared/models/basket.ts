import * as uuid from 'uuid';

export interface IBasketItem {
  id: string;
  productName: string;
  pictureUrl: string;
  productType: string;
  price: number;
  quantity: number;
}

export interface IBasket {
  id: string;
  items: IBasketItem[];
}

export class Basket implements IBasket {
  id: string = uuid.v4();
  items: IBasketItem[] = [];
}
