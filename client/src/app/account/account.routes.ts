import { Route } from '@angular/router';
import { LoginComponent } from '@app/account/login/login.component';
import { RegisterComponent } from '@app/account/register/register.component';

export const ACCOUNT_ROUTES: Route[] = [
  { path: 'signin', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
];
