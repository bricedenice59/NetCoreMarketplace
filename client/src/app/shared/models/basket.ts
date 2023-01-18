import { v4 as uuidv4 } from 'uuid';


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
  id: uuidv4();
  items: IBasketItem[];
}
