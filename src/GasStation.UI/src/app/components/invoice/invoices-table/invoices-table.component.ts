import {Component, OnInit} from '@angular/core';
import {InvoiceService} from "../../../services/invoice/invoice-service.service";
import {faEdit, faRemove} from "@fortawesome/free-solid-svg-icons";
import {Invoice} from "../../../models/invoice/Invoice";
import {Role} from "../../../models/enums/Role";
import {RoleAuthorisationService} from "../../../services/authentication/role-authorisation-service.service";
import {MatDialog} from "@angular/material/dialog";
import {DeleteInvoiceComponent} from "../delete-invoice/delete-invoice.component";
import {Router} from "@angular/router";

@Component({
  selector: 'app-invoices-table',
  templateUrl: './invoices-table.component.html',
  styleUrls: ['./invoices-table.component.css']
})
export class InvoicesTableComponent implements OnInit{
  private editors: Role[] = [Role.Admin, Role.User];
  invoices?: Invoice[];
  faEdit = faEdit;
  faDelete = faRemove;
  constructor(private invoiceService: InvoiceService,
              private roleAuthService: RoleAuthorisationService,
              private dialogWindow: MatDialog,
              private router: Router) {}
  ngOnInit(): void {
    this.invoiceService.GetAll()
      .subscribe(invoices => this.invoices = invoices);
  }


  get canAdd() : boolean {return this.roleAuthService.isHasRole(this.editors);}
  get canEdit() : boolean {return this.roleAuthService.isHasRole(this.editors);}
  get canDelete() : boolean {return this.roleAuthService.isHasRole(this.editors);}

  public async editInvoice(invoice: Invoice){
    this.router.navigate(['/update-invoice'], {state: invoice});
  }

  public async openDeleteWindow(invoice: Invoice){
    const dialogRef = this.dialogWindow.open(DeleteInvoiceComponent, {
      width: '480px',
      data: invoice
    });

    dialogRef.componentInstance.invoiceDeleted.subscribe((invoiceId) => {
      this.invoices = this.invoices?.filter(inv => inv.id != invoiceId);
    })
  }


}
