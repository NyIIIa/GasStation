import {Component, OnInit} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {ToastrService} from "ngx-toastr";
import {ReportService} from "../../../services/report/report.service";
import {Router} from "@angular/router";

@Component({
  selector: 'app-add-report',
  templateUrl: './add-report.component.html',
  styleUrls: ['./add-report.component.css']
})
export class AddReportComponent implements OnInit {
  addReportForm!: FormGroup;
  submitted = false;

  constructor(private formBuilder: FormBuilder,
              private toast: ToastrService,
              private reportService: ReportService,
              private router: Router) {
  }

  ngOnInit(): void {
    this.addReportForm = this.formBuilder.group({
      title: ['', [Validators.required, Validators.minLength(5), Validators.maxLength(60)]],
      startDate: ['', [Validators.required]],
      endDate: ['', [Validators.required]],
      transactionType: ['', [Validators.required]]
    });
  }

  // convenience getter for easy access to form fields
  get f() {
    return this.addReportForm.controls;
  }

  onSubmit() {
    this.submitted = true;

    // stop here if form is invalid
    if (this.addReportForm.invalid) {
      this.toast.warning('The add report form should be valid!', 'Validation form warning!');

      return;
    } else {
      this.reportService.Add({
        title: this.addReportForm.controls['title'].value,
        startDate: new Date(this.addReportForm.controls['startDate'].value).getTime(),
        endDate: new Date(this.addReportForm.controls['endDate'].value).getTime(),
        transactionType: this.addReportForm.controls['transactionType'].value
      }).subscribe(res => {
        this.toast.success('The report has been successfully added!');
        this.router.navigate(['/reports']);
      });
    }
  }
}
