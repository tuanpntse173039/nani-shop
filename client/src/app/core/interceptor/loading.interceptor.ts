import { HttpEvent, HttpHandlerFn, HttpRequest } from '@angular/common/http';
import { inject } from '@angular/core';
import { delay, finalize, Observable } from 'rxjs';
import { BusyService } from '../services/busy.service';

export function loadingInterceptor(req: HttpRequest<unknown>, next: HttpHandlerFn): Observable<HttpEvent<unknown>> {
  const busyService = inject(BusyService);
  busyService.busy();
  return next(req).pipe(
    delay(500),
    finalize(() => {
      busyService.idle();
    })
  );
}
