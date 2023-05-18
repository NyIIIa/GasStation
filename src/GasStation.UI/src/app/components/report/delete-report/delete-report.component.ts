import {Component, EventEmitter, Inject, Output} from '@angular/core';
import {ReportService} from "../../../services/report/report.service";
import {MAT_DIALOG_DATA, MatDialogRef} from "@angular/material/dialog";
import {Report} from "../../../models/report/Report";

@Component({
  selector: 'app-delete-report',
  templateUrl: './delete-report.component.html',
  styleUrls: ['./delete-report.component.css']
})
export class DeleteReportComponent {
  @Output() reportDeleted = new EventEmitter<number>();

  constructor(private reportService: ReportService,
              public dialogRef: MatDialogRef<DeleteReportComponent>,
              @Inject(MAT_DIALOG_DATA) public report: Report) {}

  onClose(){
    this.dialogRef.close();
  }

  deleteReport(){
    this.reportService.Delete({id: this.report.id})
      .subscribe(res => {
        this.dialogRef.close();
        this.reportDeleted.emit(this.report.id);
      });
  }
}
