import { Component, OnInit } from '@angular/core';
import { environments } from 'src/environment';
import { BasketService } from './basket/basket.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent implements OnInit {
  title = 'client';

  constructor(private basketService: BasketService) {}

  ngOnInit(): void {
    const currentBasketId = localStorage.getItem(
      environments.basketNameLocalStorage
    );
    if (currentBasketId) {
      this.basketService.getBasket(currentBasketId).subscribe(
        () => {
          console.log('Basket loaded...');
        },
        (error) => {
          console.error(error);
        }
      );
    }
  }
}
