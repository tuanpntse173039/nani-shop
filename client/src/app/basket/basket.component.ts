import { CommonModule } from '@angular/common';
import { ChangeDetectionStrategy, Component, type OnInit } from '@angular/core';
import { RouterLink } from '@angular/router';
import { IBasket, IBasketItem } from '@shared/models/basket';
import { Observable } from 'rxjs';
import { OrderTotalsComponent } from '../shared/components/order-totals/order-totals.component';
import { BasketService } from './basket.service';

@Component({
  selector: 'app-basket',
  standalone: true,
  imports: [CommonModule, OrderTotalsComponent, RouterLink],
  templateUrl: './basket.component.html',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class BasketComponent implements OnInit {
  public basket$: Observable<IBasket | null> | undefined;
  constructor(private basketService: BasketService) {}
  ngOnInit(): void {
    this.basket$ = this.basketService.basket$;
  }

  public incrementBasketItem(basketItem: IBasketItem) {
    this.basketService.incrementBasketItem(basketItem);
  }

  public decrementBasketItem(basketItem: IBasketItem) {
    this.basketService.decrementBasketItem(basketItem);
  }

  public removeBasketItem(basketItem: IBasketItem) {
    this.basketService.removeItemFromBasket(basketItem);
  }
}
