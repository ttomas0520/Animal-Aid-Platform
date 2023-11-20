import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink, RouterLinkActive, RouterOutlet } from '@angular/router';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { faEdit, faTrash } from '@fortawesome/free-solid-svg-icons';
import { CategoryModel } from '../../../core/models/category-model';
import { AdminService } from '../../../core/services/admin.service';

@Component({
  selector: 'app-category-list',
  standalone: true,
  imports: [
    CommonModule,
    RouterOutlet,
    RouterLink,
    RouterLinkActive,
    FontAwesomeModule,
  ],
  templateUrl: './category-list.component.html',
  styleUrl: './category-list.component.css',
})
export class CategoryListComponent implements OnInit {
  faEdit = faEdit;
  faTrash = faTrash;

  categories: CategoryModel[] = [];

  constructor(private adminService: AdminService) {}
  ngOnInit() {
    console.log('betoltotem');
    this.adminService.getAllCategory().subscribe({
      next: (resp) => {
        this.categories = resp;
        console.log(resp);
      },
      error: (error) => {
        console.log(error);
      },
    });
  }

  onDeleteCategory(category: CategoryModel) {
    this.adminService.deleteCategory(category).subscribe({
      next: (resp) => {
        console.log(resp);
      },
      error: (error) => {
        console.log(error);
      },
    });
  }
}
