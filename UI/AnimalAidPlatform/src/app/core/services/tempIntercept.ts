import { inject, Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor, HttpErrorResponse } from '@angular/common/http';
import { catchError, Observable, throwError } from 'rxjs';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import axios from 'axios';

@Injectable()

export class AxiosInterceptor {
  private _snackBar: MatSnackBar;

  constructor(snackBar: MatSnackBar) {
    this._snackBar = snackBar;

    // Axios interceptor hozzáadása
    axios.interceptors.response.use(
      response => {
        // Sikeres válasz, visszaadjuk a válasz adatokat
        return response;
      },
      error => {
        // Hiba esetén
        if (error.response && error.response.status === 0) {
          this._snackBar.open('Hálózati hiba. Kérjük, ellenőrizze az internetkapcsolatot!', 'Bezár', {
            duration: 3000,
          });
        } else if (error.response) {
          this._snackBar.open(`Hiba történt: ${error.response.status} - ${error.response.statusText}`, 'Bezár', {
            duration: 3000,
          });
        }
        // Hiba dobása
        return Promise.reject(error);
      }
    );
  }
}
