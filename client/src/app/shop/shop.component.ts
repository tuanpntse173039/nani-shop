import { Component, OnInit } from '@angular/core';
import { ShopService } from './shop.service';
import { IProduct } from '../shared/models/product';

@Component({
  selector: 'app-shop',
  standalone: true,
  imports: [],
  templateUrl: './shop.component.html'
})
export class ShopComponent implements OnInit {
  public products : IProduct[] = [];

  constructor(private shopService : ShopService) { }

  ngOnInit(): void {
    this.shopService.getProducts().subscribe({
      next: response => {
        this.products = response.data;
      },
      error: error => {
        console.log(error);
      },
      complete: () => {
        console.log('complete');
      }
    });
  }


}
