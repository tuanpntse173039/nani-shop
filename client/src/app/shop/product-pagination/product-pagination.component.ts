import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-product-pagination',
  standalone: true,
  imports: [],
  templateUrl: './product-pagination.component.html'
})
export class ProductPaginationComponent {
  @Input() page: number = 1;
}
