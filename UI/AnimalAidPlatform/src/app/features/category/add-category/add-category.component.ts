import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink, RouterLinkActive, RouterOutlet } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { CategoryModel } from '../../../core/models/category-model';
import { AdminService } from '../../../core/services/admin.service';
import { HttpClientModule } from '@angular/common/http';

@Component({
  selector: 'app-add-category',
  standalone: true,
  imports: [
    CommonModule,
    RouterOutlet,
    RouterLink,
    RouterLinkActive,
    FormsModule,
  ],
  templateUrl: './add-category.component.html',
  styleUrl: './add-category.component.css',
})
export class AddCategoryComponent {
  model: CategoryModel;

  constructor(private adminService: AdminService) {
    this.model = {
      name: '',
      urlHandle: '',
    };
  }
  onFormSubmit() {
    console.log('valami');
    this.adminService.addCategory(this.model).subscribe({
      next: (resp) => {
        console.log(resp);
      },
      error: (error) => {},
    });
  }
}
