import { Component, OnInit } from '@angular/core';
import { Router, RouterOutlet } from '@angular/router';
import { BasketService } from '@app/basket/basket.service';
import { NavBarComponent } from '@core/nav-bar/nav-bar.component';
import { SectionHeaderComponent } from '@core/section-header/section-header.component';
import { initFlowbite } from 'flowbite';
import { NgxSpinnerModule } from 'ngx-spinner';
import { AccountService } from './account/account.service';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, NavBarComponent, SectionHeaderComponent, NgxSpinnerModule],
  templateUrl: './app.component.html',
})
export class AppComponent implements OnInit {
  constructor(
    private basketService: BasketService,
    private accountService: AccountService,
    private router: Router
  ) {}

  ngOnInit(): void {
    initFlowbite();
    this.initialBasket();
    this.initialUser();
  }

  private initialBasket(): void {
    const basketId = localStorage.getItem('basket_id');
    if (basketId) {
      this.basketService.getBasket(basketId).subscribe({
        next: () => {
          console.log('Load basket successfully');
        },
        error: (e) => {
          console.log(e);
        },
      });
    }
  }

  private initialUser(): void {
    const token = localStorage.getItem('token');
    if (token) {
      this.accountService.loadCurrentUser(token).subscribe({
        next: () => {
          console.log('Load user successfully');
          this.router.navigateByUrl('/shop');
        },
        error: (e) => {
          console.log(e);
        },
      });
    }
  }
}
