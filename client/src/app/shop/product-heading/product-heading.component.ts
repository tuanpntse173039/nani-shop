import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-product-heading',
  standalone: true,
  imports: [],
  templateUrl: './product-heading.component.html',
})
export class ProductHeadingComponent {
  @Input({ required: true }) productCount: number = 0;
  @Input({ required: true }) pageSize: number = 0;
}
