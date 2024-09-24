import { inject, Injectable } from '@angular/core';
import {
  AuthResponseDTO,
  LoginDTO,
  RegisterDTO,
} from '../../../apiClient/data-contracts';
import { Api } from '../../../apiClient/Api';
import { environment } from '../../../environments/environment.development';
import { Observable } from 'rxjs';
import { HttpResponse } from '../../../apiClient/http-client';
import { ApiService } from './api.service';
import { JwtHelperService } from '@auth0/angular-jwt';
import {
  ActivatedRouteSnapshot,
  CanActivateFn,
  Router,
  RouterStateSnapshot,
} from '@angular/router';
const helper = new JwtHelperService();
@Injectable({
  providedIn: 'root',
})
export class AuthService {
  constructor(private apiService: ApiService, private router: Router) {}

  get isAdmin() {
    var roles = localStorage.getItem('userRoles');
    if (!roles) return false;
    return roles.includes('ADMIN');
  }
  async login(data: LoginDTO) {
    var resp = await this.apiService.api.userLoginCreate(data).catch((e) => {
      console.log(e);
    });
    if (resp && resp.data.isSucces) {
      localStorage.setItem('userToken', resp.data.token!);
      this.apiService.api.setSecurityData(resp.data.token);
      var details = await this.apiService.api.userDetailList();
      localStorage.setItem('userRoles', JSON.stringify(details.data.roles));
      localStorage.setItem('userName', details.data.name!);
      this.router.navigate(['home']);
    } else {
      this.router.navigate(['error']);
    }
  }

  logout() {
    localStorage.clear();
    this.router.navigate(['user/login']);
  }

  async register(data: RegisterDTO) {
    data.roles = ['ADMIN'];
    var token = await this.apiService.api.userRegisterCreate(data);
    console.log(token);
  }

  isAuthenticated(): boolean {
    var token = this.getAuthorizationToken();
    var permission = !helper.isTokenExpired(token);
    return permission;
  }

  getUserName() {
    var name = localStorage.getItem('userName');
    return name;
  }

  getAuthorizationToken() {
    var token = localStorage.getItem('userToken');
    return token;
  }
}
