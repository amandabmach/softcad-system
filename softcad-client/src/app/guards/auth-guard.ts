import { Injectable, inject } from '@angular/core';
import { CanActivateFn } from '@angular/router';
import { Router } from '@angular/router';
import { AuthenticatorService } from '../services/authenticator.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard {
  constructor() { }
}

export const authGuard: CanActivateFn = () => {
  
  return inject(AuthenticatorService).userAuthenticated()
    ? true
    : inject(Router).navigate(['/login']);
};