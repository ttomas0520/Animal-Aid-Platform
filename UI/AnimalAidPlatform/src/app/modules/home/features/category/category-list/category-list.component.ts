import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink, RouterLinkActive, RouterOutlet } from '@angular/router';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { faEdit, faTrash } from '@fortawesome/free-solid-svg-icons';
import { CategoryModel } from '../../../../../core/models/category-model';
import { AdminService } from '../../../../../core/services/admin.service';
import { FeedPostService } from '../../../../../core/services/feedPost.service';
import { CategoryDto } from '../../../../../../apiClient/data-contracts';

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
  categories: CategoryDto[] = [];
  constructor(
    private adminService: AdminService,
    private feedPostService: FeedPostService
  ) {}
  ngOnInit() {
    this.feedPostService.getCategories().then((resp) => {
      this.categories = [...resp];
    });
  }

  onDeleteCategory(category: CategoryModel) {}
}
