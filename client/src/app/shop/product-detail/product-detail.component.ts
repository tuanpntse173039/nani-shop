import { IProduct } from '@/app/shared/models/product';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ShopService } from '../shop.service';
import { CurrencyPipe } from '@angular/common';

@Component({
  selector: 'app-product-detail',
  standalone: true,
  imports: [CurrencyPipe],
  templateUrl: './product-detail.component.html',
})
export class ProductDetailComponent implements OnInit {
  public product: IProduct | null = null;
  public quantity: number = 0;

  constructor(
    private shopService: ShopService,
    private activatedRoute: ActivatedRoute,
  ) {}

  ngOnInit(): void {
    this.loadProduct();
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
}
