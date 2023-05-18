import {Component, OnInit} from '@angular/core';
import {Invoice} from "../../../models/invoice/Invoice";
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {Router} from "@angular/router";
import {InvoiceService} from "../../../services/invoice/invoice-service.service";
import {ToastrService} from "ngx-toastr";

@Component({
  selector: 'app-update-invoice',
  templateUrl: './update-invoice.component.html',
  styleUrls: ['./update-invoice.component.css']
})
export class UpdateInvoiceComponent implements OnInit {
  invoiceToEdit!: Invoice;
  invoiceEditForm!: FormGroup;
  submitted = false;


  constructor(private formBuilder: FormBuilder,
              private invoiceService: InvoiceService,
              private router: Router,
              private toast: ToastrService) {
  }

  ngOnInit() {
    this.invoiceToEdit = history.state;

    this.invoiceEditForm = this.formBuilder.group({
      id: [this.invoiceToEdit?.id],
      newTitle: ['', [Validators.required]],
      transactionType: [this.invoiceToEdit?.transactionType, [Validators.required]],
      consumer: [this.invoiceToEdit?.consumer, [Validators.required]],
      provider: [this.invoiceToEdit?.provider, [Validators.required]],
      totalFuelQuantity: [this.invoiceToEdit?.totalFuelQuantity, [Validators.required]]
    });
  }

  // convenience getter for easy access to form fields
  get f() {
    return this.invoiceEditForm.controls;
  }

  onSubmit() {
    this.submitted = true;

    // stop here if form is invalid
    if (this.invoiceEditForm.invalid) {
      this.toast.warning('The update invoice form should be valid!', 'Validation form warning!');

      return;
    } else {
      this.invoiceService.Update(this.invoiceEditForm.value)
        .subscribe(res =>
          this.toast.success('The invoice has been successfully updated'));
      this.router.navigate(['/invoices'], {
        queryParams: {
          duration: 4000
        }
      });
    }
  }
}
