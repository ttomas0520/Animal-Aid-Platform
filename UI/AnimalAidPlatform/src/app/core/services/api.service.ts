import { Injectable } from '@angular/core';
import { Api } from '../../../apiClient/Api';
import { environment } from '../../../environments/environment.development';
import { MatSnackBar } from '@angular/material/snack-bar';
import {
  AuthResponseDTO,

} from '../../../apiClient/data-contracts';

@Injectable({
  providedIn: 'root',
})
export class ApiService {
  api: Api;
  constructor(private snackBar: MatSnackBar) {
    this.api = new Api({
      baseURL: environment.apiBaseUrl,
      securityWorker: (token) =>
        token ? { headers: { Authorization: `Bearer ${token}` } } : {}
    })

    // Axios Interceptor setup for token handling and response errors
    this.api.instance.interceptors.request.use(
      async (config) => {
        // Handle token in the header
        return config;
      },
      (error) => {
        return Promise.reject(error);
      }
    );

    this.api.instance.interceptors.response.use(
      (response) => response,
      async (error) => {
        const isAuthError = (value: AuthResponseDTO): value is AuthResponseDTO => {
          if (value.message)
            return true
          else
            return false;
        }
        if (isAuthError(error.response.data)) {
          this.snackBar.open(`Hiba történt: ${error.response.status} - ${error.response.data.message}`, 'Bezár', {
            duration: 3000,
          });
        } else {
          this.snackBar.open(`Hiba történt: ${error.response.status} - ${error.response.data}`, 'Bezár', {
            duration: 3000,
          });
        }

        return Promise.reject(error);
      }
    );
    var possibleToken = localStorage.getItem('userToken')
    if (possibleToken) {
      this.api.setSecurityData(possibleToken)
    }
  }
}
