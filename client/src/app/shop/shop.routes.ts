import { Route } from '@angular/router';
import { ProductDetailComponent } from '@shop/product-detail/product-detail.component';
import { ShopComponent } from '@shop/shop.component';

export const SHOP_ROUTES: Route[] = [
  { path: '', component: ShopComponent },
  { path: ':id', component: ProductDetailComponent },
];
