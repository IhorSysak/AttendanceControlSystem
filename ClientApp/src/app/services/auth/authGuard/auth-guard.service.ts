import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthService } from '../auth.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuardService implements CanActivate {

  constructor(private router: Router, private authService: AuthService) { }

  canActivate(
    route: ActivatedRouteSnapshot, 
    state: RouterStateSnapshot): boolean | UrlTree | Observable<boolean | UrlTree> | Promise<boolean | UrlTree> {

      if(localStorage.getItem('authToken') !== null) {
        const role = route.data["roles"] as Array<string>;
        if(role) {
          const isMatch = this.authService.roleMatch(role);
          if(isMatch) {
            return true;
          } else {
            this.router.navigate(['forbidden']);
            return false;
          }
        }
      }
      
      this.router.navigate(['/login']);
      return true;
  }
}
