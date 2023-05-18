import { Component } from '@angular/core';
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {ToastrService} from "ngx-toastr";
import {FuelService} from "../../../services/fuel/fuel-service.service";
import {Router} from "@angular/router";
import {InvoiceService} from "../../../services/invoice/invoice-service.service";

@Component({
  selector: 'app-add-invoice',
  templateUrl: './add-invoice.component.html',
  styleUrls: ['./add-invoice.component.css']
})
export class AddInvoiceComponent {
  addInvoiceForm!: FormGroup;
  submitted = false;

  constructor(private formBuilder: FormBuilder,
              private toast: ToastrService,
              private invoiceService: InvoiceService,
              private router: Router) {

  }

  ngOnInit(): void {
    this.addInvoiceForm = this.formBuilder.group({
      title: ['', [Validators.required, Validators.minLength(5), Validators.maxLength(30)]],
      transactionType: [0, [Validators.required]],
      consumer: ['', [Validators.required, Validators.minLength(5), Validators.maxLength(30)]],
      provider: ['', [Validators.required, Validators.minLength(5), Validators.maxLength(30)]],
      totalFuelQuantity: ['', [Validators.required, Validators.min(0)]],
      fuelTitle: ['', [Validators.required]],
    });
  }

  // convenience getter for easy access to form fields
  get f() {
    return this.addInvoiceForm.controls;
  }

  onSubmit() {
    this.submitted = true;

    // stop here if form is invalid
    if (this.addInvoiceForm.invalid) {
      this.toast.warning('The add fuel form should be valid!', 'Validation form warning!');

      return;
    } else {
      this.invoiceService.Add(this.addInvoiceForm.value)
        .subscribe(res => {
          this.toast.success('The invoice has been successfully added!');
          this.router.navigate(['/invoices']);
        });
    }
  }
}
