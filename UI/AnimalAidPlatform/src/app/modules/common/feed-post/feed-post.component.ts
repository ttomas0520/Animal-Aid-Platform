import { Component, EventEmitter, Input, Output } from '@angular/core';
import { ImportModule } from '../import.module';
import { FeedPostResponseDTO, Report, ReportDTO } from '../../../../apiClient/data-contracts';
import { NgOptimizedImage } from '@angular/common';
import { FeedPostService } from '../../../core/services/feedPost.service';
import { MatDialog } from '@angular/material/dialog';
import { ReportDialogComponent } from '../../../core/modals/report-dialog/report-dialog.component';
import { AdminService } from '../../../core/services/admin.service';
import { AdminResolveReportDialogComponent } from '../../../core/modals/admin-resolve-report-dialog/admin-resolve-report-dialog.component';
import { ImagePreviewDialogComponent } from '../../../core/modals/image-preview-dialog/image-preview-dialog.component';

@Component({
  selector: 'app-feed-post',
  standalone: true,
  imports: [ImportModule, NgOptimizedImage],
  templateUrl: './feed-post.component.html',
  styleUrl: './feed-post.component.css',
})
export class FeedPostComponent {
  @Input() post?: FeedPostResponseDTO;
  @Input() isAdminMode: boolean = false;
  @Output() postRefresh = new EventEmitter<number>();
  reports: Report[] | null = null; // Initialize as null for lazy loading
  isHovered: boolean = false;
  constructor(protected feedpostService: FeedPostService, private dialog: MatDialog, protected adminService:AdminService){}
  
  like(){
    var likes = this.post?.likeNumber!
    
    this.feedpostService.likePost(this.post!.id!).then((resp) =>{
      likes = resp;
      if (this.post) {
        this.post.likeNumber = likes;
        this.post.isLiked = !this.post.isLiked
      }
    })

  }

  delete(){
    this.feedpostService.deletePostById(this.post!.id!).then(resp =>
      this.postRefresh.emit(this.post!.id!)
    )
  }

  openReportDialog(): void {
    const dialogRef = this.dialog.open(ReportDialogComponent, {
      width: '400px'
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.submitReport(result);
      }
    });
  }

  submitReport(reason: string): void {
    // Send the report to the backend, using reason as the report justification
    console.log('Report reason:', reason);
    const reportDTO: ReportDTO = {
      feedPostId: this.post!.id!, 
      reason: reason
    };
    this.feedpostService.reportPostById(reportDTO)
  }

  fetchReports(): void {
    if (!this.reports) { // Fetch only if reports haven't been loaded
      this.adminService.getReportsForPost(this.post!.id!)
        .then(
          (data) => {
            this.reports = data;
          })
          .catch(error => {
            console.error('Failed to fetch reports', error);
          })
        };
  }

  openReportDecisionDialog(report: Report): void {
    const dialogRef = this.dialog.open(AdminResolveReportDialogComponent);
  
    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.adminService.resolveReport(report.id!, result)
      }
    });
  }

  openImagePreview(imageUrl: string): void {
    this.dialog.open(ImagePreviewDialogComponent, {
      data: { imageUrl },
      panelClass: 'custom-dialog-container'
    });
  }
}
