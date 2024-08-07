import { Component, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { NavBarComponent } from '@core/nav-bar/nav-bar.component';
import { SectionHeaderComponent } from '@core/section-header/section-header.component';
import { initFlowbite } from 'flowbite';
import { NgxSpinnerModule } from 'ngx-spinner';
import { BasketService } from './basket/basket.service';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    RouterOutlet,
    NavBarComponent,
    SectionHeaderComponent,
    NgxSpinnerModule,
  ],
  templateUrl: './app.component.html',
})
export class AppComponent implements OnInit {
  constructor(private basketService: BasketService) {}

  ngOnInit(): void {
    initFlowbite();
    this.initialBasket();
  }

  private initialBasket(): void {
    const basketId = localStorage.getItem('basket_id');
    if (basketId) {
      this.basketService.getBasket(basketId);
    }
  }
}
