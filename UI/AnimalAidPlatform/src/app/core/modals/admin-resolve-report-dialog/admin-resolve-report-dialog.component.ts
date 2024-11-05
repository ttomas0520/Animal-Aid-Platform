import { Component } from '@angular/core';
import { ImportModule } from '../../../modules/common/import.module';
import { ResolveReportDTO } from '../../../../apiClient/data-contracts';
import { MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-admin-resolve-report-dialog',
  standalone: true,
  imports: [ImportModule],
  templateUrl: './admin-resolve-report-dialog.component.html',
  styleUrl: './admin-resolve-report-dialog.component.css'
})
export class AdminResolveReportDialogComponent {
  reportDecision: ResolveReportDTO = {
    adminResponse: '',
    isResolved: true,
    reportAction: 'None',
  };

  constructor(public dialogRef: MatDialogRef<AdminResolveReportDialogComponent>) {}

  onCancel(): void {
    this.dialogRef.close();
  }

  onSubmit(): void {
    this.dialogRef.close(this.reportDecision);
  }
}
