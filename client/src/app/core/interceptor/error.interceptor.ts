import { HttpEvent, HttpHandlerFn, HttpRequest } from '@angular/common/http';
import { inject } from '@angular/core';
import { NavigationExtras, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { catchError, Observable } from 'rxjs';

export function errorInterceptor(req: HttpRequest<unknown>, next: HttpHandlerFn): Observable<HttpEvent<unknown>> {
  const router = inject(Router);
  const toast = inject(ToastrService);
  return next(req).pipe(
    catchError((err) => {
      if (err.status === 400) {
        if (err.error.errors) {
          throw err.error;
        } else {
          toast.error(err.error.message, err.error.statusCode + ' Bad request');
        }
      }
      if (err.status === 401) {
        toast.error(err.error.message, err.error.statusCode + ' Bad request');
      }
      if (err.status === 404) {
        router.navigateByUrl('/not-found');
      }
      if (err.status === 500) {
        const navigationExtras: NavigationExtras = { state: { error: err.error } };
        router.navigateByUrl('/server-error', navigationExtras);
      }
      throw err;
    })
  );
}
