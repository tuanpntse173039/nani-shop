import { BasketService } from '@/app/basket/basket.service';
import { CurrencyPipe } from '@angular/common';
import { Component, Input, OnInit } from '@angular/core';
import { RouterLink } from '@angular/router';
import { IProduct } from '@shared/models/product';

@Component({
  selector: 'app-product-item',
  standalone: true,
  imports: [CurrencyPipe, RouterLink],
  templateUrl: './product-item.component.html',
})
export class ProductItemComponent implements OnInit {
  @Input({ required: true }) product: IProduct | undefined;

  ngOnInit(): void {}

  constructor(private basketService: BasketService) {}

  addItemToBasket(): void {
    if (this.product) {
      this.basketService.addItemToBasket(this.product);
    }
  }
}
