import { Component } from '@angular/core';
import { FeedComponent } from '../home/feed/feed.component';
import { ImportModule } from '../../../common/import.module';
import { FeedPostService } from '../../../../core/services/feedPost.service';
import {
  FeedPostResponseDTO,
  UserDetailDTO,
} from '../../../../../apiClient/data-contracts';
import { AdminService } from '../../../../core/services/admin.service';
import { AddCategoryComponent } from '../category/add-category/add-category.component';
import { CategoryListComponent } from '../category/category-list/category-list.component';

@Component({
  selector: 'app-admin-menu',
  standalone: true,
  templateUrl: './admin-menu.component.html',
  styleUrl: './admin-menu.component.css',
  imports: [
    FeedComponent,
    ImportModule,
    AddCategoryComponent,
    CategoryListComponent,
  ],
})
export class AdminMenuComponent {
  public postList: FeedPostResponseDTO[] = [];

  users: UserDetailDTO[] = [];

  searchTerm: string = '';

  filteredUsers(): UserDetailDTO[] {
    return this.users.filter((user) =>
      user.name!.toLowerCase().includes(this.searchTerm.toLowerCase())
    );
  }

  constructor(
    private postService: FeedPostService,
    private adminService: AdminService
  ) {
    postService.getPosts().then((resp) => {
      this.postList = [...resp];
    });

    this.adminService.getUsers().then((resp) => {
      this.users = [...resp];
    });
  }

  selectUser(user: UserDetailDTO) {
    this.adminService.getPostCreatedByUser(user.id!).then((resp) => {
      this.searchTerm = user.name!;
      this.postList = [...resp];
    });
  }
}
