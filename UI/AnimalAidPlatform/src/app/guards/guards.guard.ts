import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AuthService } from '../core/services/auth.service';

export const authGuard: CanActivateFn = (route, state) => {
  const router = inject(Router);
  const authService = inject(AuthService);
  if (!authService.isAuthenticated()) {
    router.navigate(['user/login']);
    return false;
  }
  return true;
};

export const adminGuard:CanActivateFn =(route, state) =>{
  const router = inject(Router);
  const authService = inject(AuthService);
  if(!authService.isAdmin){
    router.navigate(['error']);
    return false;
  }
  return true
}
