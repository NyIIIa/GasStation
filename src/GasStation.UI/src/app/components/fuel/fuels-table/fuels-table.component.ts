import {Component, OnInit} from '@angular/core';
import {RoleAuthorisationService} from "../../../services/authentication/role-authorisation-service.service";
import {faEdit} from "@fortawesome/free-solid-svg-icons/faEdit";
import {Role} from "../../../models/enums/Role";
import {Fuel} from "../../../models/fuel/Fuel";
import {FuelService} from "../../../services/fuel/fuel-service.service";
import {Router} from "@angular/router";


@Component({
  selector: 'app-fuels-table',
  templateUrl: './fuels-table.component.html',
  styleUrls: ['./fuels-table.component.css']
})
export class FuelsTableComponent implements OnInit{
  private editors: Role[] = [Role.Admin];
  fuels: Fuel[] = [];
  faEdit = faEdit;

  constructor(private roleAuthService: RoleAuthorisationService,
              private fuelService: FuelService,
              private router: Router) {
  }

  ngOnInit(): void {
    this.fuelService.GetAll()
      .subscribe(fuels => this.fuels = fuels);
  }

  get canAdd() : boolean {return this.roleAuthService.isHasRole(this.editors);}
  get canEdit() : boolean {return this.roleAuthService.isHasRole(this.editors);}

  public async editFuel(fuel: Fuel){
      this.router.navigate(['/update-fuel'], {state: fuel});
  }
}
