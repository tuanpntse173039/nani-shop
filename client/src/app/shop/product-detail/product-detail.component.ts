import { BasketService } from '@/app/basket/basket.service';
import { IProduct } from '@/app/shared/models/product';
import { CurrencyPipe } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ShopService } from '../shop.service';

@Component({
  selector: 'app-product-detail',
  standalone: true,
  imports: [CurrencyPipe],
  templateUrl: './product-detail.component.html',
})
export class ProductDetailComponent implements OnInit {
  public product: IProduct | null = null;
  public quantity = 1;

  constructor(
    private shopService: ShopService,
    private activatedRoute: ActivatedRoute,
    private basketService: BasketService
  ) {}

  ngOnInit(): void {
    this.loadProduct();
  }

  public addToCard() {
    if (!this.product || this.quantity === 0) return;
    this.basketService.addItemToBasket(this.product, this.quantity);
  }

  private loadProduct(): void {
    const productId = this.activatedRoute.snapshot.paramMap.get('id');

    if (productId === null) {
      return;
    }

    this.shopService.getProduct(+productId).subscribe({
      next: (res) => {
        this.product = res;
      },
      error: (err) => {
        console.log(err);
      },
    });
  }

  public incrementQuantity() {
    this.quantity++;
  }

  public decrementQuantity() {
    if (this.quantity === 1) return;
    this.quantity--;
  }
}
