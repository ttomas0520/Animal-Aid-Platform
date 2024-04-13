import { Injectable } from '@angular/core';
import { Api } from '../../../apiClient/Api';
import { environment } from '../../../environments/environment.development';

@Injectable({
  providedIn: 'root',
})
export class ApiService {
  api: Api;
  constructor() {
    this.api = new Api({
        baseUrl: environment.apiBaseUrl,
        securityWorker: (token) =>
          token ? { headers: { Authorization: `Bearer ${token}` } } : {}
      })
    var possibleToken = localStorage.getItem('userToken')
    if(possibleToken){
        console.log("Van token")
        this.api.setSecurityData(possibleToken)
    }
  }
}
