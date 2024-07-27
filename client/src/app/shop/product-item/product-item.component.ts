import { Component, Input, OnInit } from '@angular/core';
import { IProduct } from '@shared/models/product';

@Component({
  selector: 'app-product-item',
  standalone: true,
  imports: [],
  templateUrl: './product-item.component.html',
})
export class ProductItemComponent implements OnInit {
  @Input({ required: true }) product: IProduct | undefined;

  ngOnInit(): void {
  }
}
