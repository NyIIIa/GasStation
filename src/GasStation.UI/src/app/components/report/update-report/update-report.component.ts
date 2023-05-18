import {Component, OnInit} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {ReportService} from "../../../services/report/report.service";
import {Router} from "@angular/router";
import {ToastrService} from "ngx-toastr";
import {Report} from "../../../models/report/Report";

@Component({
  selector: 'app-update-report',
  templateUrl: './update-report.component.html',
  styleUrls: ['./update-report.component.css']
})
export class UpdateReportComponent implements OnInit{
  reportToEdit!: Report;
  reportEditForm!: FormGroup;
  submitted = false;

  constructor(private formBuilder: FormBuilder,
              private reportService: ReportService,
              private router: Router,
              private toast: ToastrService) {
  }

  ngOnInit(): void {
    this.reportToEdit = history.state;

    this.reportEditForm = this.formBuilder.group({
      id: [this.reportToEdit.id],
      newTitle: ['', [Validators.required, Validators.minLength(5), Validators.maxLength(60)]]
    });
  }

  // convenience getter for easy access to form fields
  get f() {
    return this.reportEditForm.controls;
  }

  onSubmit() {
    this.submitted = true;

    // stop here if form is invalid
    if (this.reportEditForm.invalid) {
      this.toast.warning('The update report form should be valid!', 'Validation form warning!');

      return;
    } else {
      this.reportService.Update(this.reportEditForm.value)
        .subscribe(res =>
          this.toast.success('The report has been successfully updated'));
      this.router.navigate(['/reports'], {
        queryParams: {
          duration: 4000
        }
      });
    }
  }
}
