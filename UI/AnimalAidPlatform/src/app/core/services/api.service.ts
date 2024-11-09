import { Injectable } from '@angular/core';
import { Api } from '../../../apiClient/Api';
import { environment } from '../../../environments/environment.development';
import { MatSnackBar } from '@angular/material/snack-bar';

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
        this.snackBar.open(`Hiba történt: ${error.response.status} - ${error.response.data}`, 'Bezár', {
          duration: 3000,
        });
        return Promise.reject(error);
      }
    );
    var possibleToken = localStorage.getItem('userToken')
    if (possibleToken) {
      this.api.setSecurityData(possibleToken)
    }
  }
}
