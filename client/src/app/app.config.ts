import { ApplicationConfig, provideZoneChangeDetection } from '@angular/core';
import { provideRouter } from '@angular/router';

import { provideHttpClient, withInterceptors } from '@angular/common/http';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { routes } from '@app/app.routes';
import { errorInterceptor } from '@core/interceptor/error.interceptor';
import { provideToastr } from 'ngx-toastr';
import { loadingInterceptor } from './core/interceptor/loading.interceptor';

export const appConfig: ApplicationConfig = {
  providers: [
    provideZoneChangeDetection({ eventCoalescing: true }),
    provideRouter(routes),
    provideAnimationsAsync(),
    provideHttpClient(withInterceptors([errorInterceptor, loadingInterceptor])),
    provideToastr({
      timeOut: 7000,
      positionClass: 'toast-bottom-right',
      preventDuplicates: false,
    }),
    // provideSpinnerConfig({
    //   type: 'pacman',
    // }),
  ],
};
