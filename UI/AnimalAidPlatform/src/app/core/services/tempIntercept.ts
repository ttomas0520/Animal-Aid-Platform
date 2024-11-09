import { inject, Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor, HttpErrorResponse } from '@angular/common/http';
import { catchError, Observable, throwError } from 'rxjs';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';

@Injectable()
export class SampleInterceptor implements HttpInterceptor {
    private _snackBar = inject(MatSnackBar);
    constructor() { }
    intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
        return next.handle(request).pipe(
          catchError((error: HttpErrorResponse) => {
            if (error.status === 0) {
              this._snackBar.open('Hálózati hiba. Kérjük, ellenőrizze az internetkapcsolatot!', 'Bezár', {
                duration: 3000,
              });
            } else {
              this._snackBar.open(`Hiba történt: ${error.status} - ${error.message}`, 'Bezár', {
                duration: 3000,
              });
            }
            return throwError(() => error);
          })
        );
      }
}