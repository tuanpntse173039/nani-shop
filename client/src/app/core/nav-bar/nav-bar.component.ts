import { AccountService } from '@/app/account/account.service';
import { BasketService } from '@/app/basket/basket.service';
import { IBasket } from '@/app/shared/models/basket';
import { IUser } from '@/app/shared/models/user';
import { CommonModule } from '@angular/common';
import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
import { RouterLink, RouterLinkActive, RouterOutlet } from '@angular/router';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-nav-bar',
  standalone: true,
  imports: [RouterOutlet, RouterLink, RouterLinkActive, CommonModule],
  templateUrl: './nav-bar.component.html',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class NavBarComponent implements OnInit {
  public basket$: Observable<IBasket | null> | undefined;
  public currentUser$: Observable<IUser | null> | undefined;

  constructor(
    private basketService: BasketService,
    private accountService: AccountService
  ) {}

  ngOnInit(): void {
    this.basket$ = this.basketService.basket$;
    this.currentUser$ = this.accountService.currentUser$;
  }

  public onLogout(): void {
    this.accountService.logout();
  }
}
