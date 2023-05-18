import {Component, OnInit} from '@angular/core';
import {Report} from "../../../models/report/Report";

@Component({
  selector: 'app-view-report-invoices',
  templateUrl: './view-report-invoices.component.html',
  styleUrls: ['./view-report-invoices.component.css']
})
export class ViewReportInvoicesComponent implements OnInit{
  reportInvoicesToView!: Report;
  ngOnInit(): void {
    this.reportInvoicesToView = history.state.report;
    //create & use date time service,
    // that will convert the unix time to date in the 'reportInvoicesToView.invoices' instance
  }
}
