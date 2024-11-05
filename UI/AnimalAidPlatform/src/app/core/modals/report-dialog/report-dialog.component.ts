import { Component } from '@angular/core';
import { ImportModule } from '../../../modules/common/import.module';
import { MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-report-dialog',
  standalone: true,
  imports: [ImportModule],
  templateUrl: './report-dialog.component.html',
  styleUrl: './report-dialog.component.css'
})
export class ReportDialogComponent {
  reason: string = '';
  showSuccessMessage: boolean = false;

  constructor(public dialogRef: MatDialogRef<ReportDialogComponent>) {}

  onCancel(): void {
    this.dialogRef.close();
  }

  onSubmit(): void {
    this.showSuccessMessage = true;

    // Wait 2 seconds before closing the dialog
    setTimeout(() => {
      this.dialogRef.close(this.reason);
    }, 2000);
  }
}
