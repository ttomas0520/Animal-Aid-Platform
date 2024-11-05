import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdminResolveReportDialogComponent } from './admin-resolve-report-dialog.component';

describe('AdminResolveReportDialogComponent', () => {
  let component: AdminResolveReportDialogComponent;
  let fixture: ComponentFixture<AdminResolveReportDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AdminResolveReportDialogComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AdminResolveReportDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
