import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpErrorResponse,
} from '@angular/common/http';
import { catchError, Observable, throwError } from 'rxjs';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
  constructor(private router: Router, private toastr: ToastrService) {}

  intercept(
    request: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    return next.handle(request).pipe(
      catchError((nextError: HttpErrorResponse) => {
        if (nextError) {
          if (nextError.status === 400) {
            if (nextError.error.errors) {
              throw nextError.error;
            } else {
              this.toastr.error(
                nextError.error.message,
                nextError.error.statusCode
              );
            }
          } else if (nextError.status === 401) {
            this.toastr.error(
              nextError.error.message,
              nextError.status.toString()
            );
          } else if (nextError.status === 404) {
            this.router.navigateByUrl('/notfound-error');
          } else if (nextError.status === 500) {
            this.router.navigateByUrl('/server-error');
          }
          return throwError(() => new Error(nextError.message));
        }
        return throwError(nextError);
      })
    );
  }
}
