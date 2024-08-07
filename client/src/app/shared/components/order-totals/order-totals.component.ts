import { BasketService } from '@/app/basket/basket.service';
import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { IBasketTotals } from '../../models/basket';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-order-totals',
  standalone: true,
  imports: [CommonModule, RouterLink],
  templateUrl: './order-totals.component.html',
})
export class OrderTotalsComponent implements OnInit {
  public basketTotal$: Observable<IBasketTotals | null> | undefined;

  constructor(private basketService: BasketService) {}

  ngOnInit(): void {
    this.basketTotal$ = this.basketService.basketTotal$;
  }
}
