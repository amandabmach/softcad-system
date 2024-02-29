import { Injectable, inject } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivateFn, RouterStateSnapshot } from '@angular/router';
import { Router } from '@angular/router';
import { AuthenticatorService } from '../services/authenticator.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard {
  constructor() { }
}

export const authGuard: CanActivateFn = (
  route: ActivatedRouteSnapshot,
  state: RouterStateSnapshot
) => {
  
  return inject(AuthenticatorService).userAuthenticated()
    ? true
    : inject(Router).navigate(['/login']);
};