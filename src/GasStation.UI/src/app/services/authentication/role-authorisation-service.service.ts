import {Injectable} from '@angular/core';
import {Role} from "../../models/enums/Role";
import {AuthService} from "./auth.service";
import {UserStoreService} from "../userstore/user-store.service";

@Injectable({
  providedIn: 'root'
})

export class RoleAuthorisationServiceService {
  private userRole!: Role;
  constructor(private userStoreService: UserStoreService) { }

  public isHasRole(roles: Role[]) : boolean{
    this.userStoreService.getRoleFromStore().subscribe(role => this.userRole = role);
    const indexOfRole = roles.indexOf(this.userRole);

    return (indexOfRole >= 0);
  }
}
