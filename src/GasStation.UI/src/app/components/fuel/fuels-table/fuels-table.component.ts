import {Component} from '@angular/core';
import {Role} from "../../../models/enums/Role";
import {RoleAuthorisationServiceService} from "../../../services/authentication/role-authorisation-service.service";

@Component({
  selector: 'app-fuels-table',
  templateUrl: './fuels-table.component.html',
  styleUrls: ['./fuels-table.component.css']
})
export class FuelsTableComponent {
  private readonly editors: Role[] = [Role.Admin];

  constructor(private readonly roleAuthService: RoleAuthorisationServiceService) {
  }

  public get CanAdd(): boolean {return this.roleAuthService.isHasRole(this.editors)}
}
