import { Routes } from '@angular/router';
import { BasketComponent } from '@app/basket/basket.component';
import { CheckoutComponent } from '@app/checkout/checkout.component';
import { HomeComponent } from '@app/home/home.component';
import { NotFoundComponent } from '@core/not-found/not-found.component';
import { ServerErrorComponent } from '@core/server-error/server-error.component';
import { TestErrorComponent } from '@core/test-error/test-error.component';

export const routes: Routes = [
  { path: '', component: HomeComponent, data: { breadcrumb: 'Home' } },
  {
    path: 'test-error',
    component: TestErrorComponent,
    data: { breadcrumb: 'Error' },
  },
  {
    path: 'not-found',
    component: NotFoundComponent,
    data: { breadcrumb: 'Not Found' },
  },
  {
    path: 'server-error',
    component: ServerErrorComponent,
    data: { breadcrumb: 'Server Error' },
  },
  {
    path: 'shop',
    loadChildren: () =>
      import('@shop/shop.routes').then((mod) => mod.SHOP_ROUTES),
    data: { breadcrumb: 'Shop' },
  },
  {
    path: 'basket',
    component: BasketComponent,
    data: { breadcrumb: 'Basket' },
  },
  {
    path: 'checkout',
    component: CheckoutComponent,
    data: { breadcrumb: 'Checkout' },
  },
  { path: '**', redirectTo: '' },
];
