import {Component, EventEmitter, Inject, Output} from '@angular/core';
import {MAT_DIALOG_DATA, MatDialogRef} from "@angular/material/dialog";
import {Invoice} from "../../../models/invoice/Invoice";
import {InvoiceService} from "../../../services/invoice/invoice-service.service";

@Component({
  selector: 'app-delete-invoice',
  templateUrl: './delete-invoice.component.html',
  styleUrls: ['./delete-invoice.component.css']
})
export class DeleteInvoiceComponent {
  @Output() invoiceDeleted = new EventEmitter<number>();

  constructor(
    private invoiceService: InvoiceService,
    public dialogRef: MatDialogRef<DeleteInvoiceComponent>,
    @Inject(MAT_DIALOG_DATA) public invoice: Invoice) {}

  onClose(){
    this.dialogRef.close();
  }

  deleteInvoice(){
    this.invoiceService.Delete({id: this.invoice.id})
      .subscribe(res => {
        this.dialogRef.close();
        this.invoiceDeleted.emit(this.invoice.id);
      });
  }
}
