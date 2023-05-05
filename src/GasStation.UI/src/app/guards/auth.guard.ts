import { Injectable } from '@angular/core';
import {ActivatedRouteSnapshot, CanActivate, Router} from '@angular/router';
import {AuthService} from "../services/authentication/auth.service";
import {ToastrService} from "ngx-toastr";
import {RoleAuthorisationServiceService} from "../services/authentication/role-authorisation-service.service";

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

  constructor(private roleAuthService: RoleAuthorisationServiceService,
              private router: Router,
              private toast: ToastrService,
              private authService: AuthService) {
  }
  canActivate(route: ActivatedRouteSnapshot){
    if(!this.authService.isLoggedIn()){
        this.toast.error('Please login first!');
        this.router.navigate(['login']);
        return false;
    }
    if(route.data['roles'] && !this.roleAuthService.isHasRole(route.data['roles'])){
      this.toast.error('You do not have the required rights!');
      this.router.navigate(['/']);
      return false;
    } else {
      return true;
    }
  }
}
