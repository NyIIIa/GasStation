import {Injectable} from '@angular/core';
import {BehaviorSubject} from "rxjs";
import {Role} from "../../models/enums/Role";
import {AuthService} from "../authentication/auth.service";

@Injectable({
  providedIn: 'root'
})
export class UserStoreService {
  private role$= new BehaviorSubject<Role>(Role.None);

  constructor(private authService: AuthService) { }

  getRoleFromStore() {
    if(this.role$.value == 0){
      if(this.authService.isLoggedIn()){
        let userPayload = this.authService.decodedJwtToken();
        this.setRoleForStore(Role[userPayload.Role as keyof typeof Role]);
      }
    }
    return this.role$.asObservable();
  }
  setRoleForStore(role: Role){
    this.role$.next(role);
  }
}
