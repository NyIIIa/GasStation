import {Component, OnInit} from '@angular/core';
import {Role} from "../../../models/enums/Role";
import {faEdit, faRemove} from "@fortawesome/free-solid-svg-icons";
import {Report} from "../../../models/report/Report";
import {ReportService} from "../../../services/report/report.service";
import {RoleAuthorisationService} from "../../../services/authentication/role-authorisation-service.service";
import {Invoice} from "../../../models/invoice/Invoice";
import {DeleteInvoiceComponent} from "../../invoice/delete-invoice/delete-invoice.component";
import {MatDialog} from "@angular/material/dialog";
import {Router} from "@angular/router";
import {DeleteReportComponent} from "../delete-report/delete-report.component";


@Component({
  selector: 'app-reports-table',
  templateUrl: './reports-table.component.html',
  styleUrls: ['./reports-table.component.css']
})
export class ReportsTableComponent implements OnInit{
  private editors: Role[] = [Role.Admin, Role.User];
  reports?: Report[];
  faEdit = faEdit;
  faDelete = faRemove;

  constructor(private reportService: ReportService,
              private roleAuthService: RoleAuthorisationService,
              private dialogWindow: MatDialog,
              private router: Router) {
  }

  ngOnInit(): void {
    this.reportService.GetAll()
      .subscribe(res =>{
        this.reports = res;
      });
  }

  get canAdd() : boolean {return this.roleAuthService.isHasRole(this.editors);}
  get canEdit() : boolean {return this.roleAuthService.isHasRole(this.editors);}
  get canDelete() : boolean {return this.roleAuthService.isHasRole(this.editors);}

  public async editReport(report: Report){
   await this.router.navigate(['/update-report'], {state: report});
  }

  public async viewReportInvoices(report: Report){
    await this.router.navigate(['/view-report-invoices'], {state: {report}})
  }

  public async openDeleteWindow(report: Report){
    const dialogRef = this.dialogWindow.open(DeleteReportComponent, {
      width: '480px',
      data: report
    });

    dialogRef.componentInstance.reportDeleted.subscribe((reportId) => {
      this.reports = this.reports?.filter(report => report.id != reportId);
    });
  }
}
