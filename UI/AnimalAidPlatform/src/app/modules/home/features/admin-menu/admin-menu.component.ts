import { Component } from '@angular/core';
import { FeedComponent } from '../home/feed/feed.component';
import { ImportModule } from '../../../common/import.module';
import { FeedPostService } from '../../../../core/services/feedPost.service';
import {
  AnimalShelterDTO,
  FeedPostResponseDTO,
  UserDetailDTO,
} from '../../../../../apiClient/data-contracts';
import { AdminService } from '../../../../core/services/admin.service';
import { AddCategoryComponent } from '../category/add-category/add-category.component';
import { CategoryListComponent } from '../category/category-list/category-list.component';
import { Router, RouterModule } from '@angular/router';
import { AnimalShelterService } from '../../../../core/services/animalShelter.service';

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
    RouterModule,
  ],
})
export class AdminMenuComponent {
  public postList: FeedPostResponseDTO[] = [];

  users: UserDetailDTO[] = [];
  shelters: AnimalShelterDTO[] =[];

  searchTerm: string = '';

  filteredUsers(): UserDetailDTO[] {
    return this.users.filter((user) =>
      user.name!.toLowerCase().includes(this.searchTerm.toLowerCase())
    );
  }

  filteredShelters(): AnimalShelterDTO[] {
    return this.shelters.filter((shelter) =>
      shelter.name.toLowerCase().includes(this.searchTerm.toLowerCase())
    );
  }

  constructor(
    private postService: FeedPostService,
    private adminService: AdminService,
    private animalShelterService: AnimalShelterService,
    private router: Router
  ) {
    this.postService.getPosts().then((resp) => {
      this.postList = [...resp];
    });

    this.adminService.getUsers().then((resp) => {
      this.users = [...resp];
    });

    this.animalShelterService.getAllAnimalShelter().then((resp) =>{
      this.shelters =[...resp]
    })
  }

  ngOnInit(){
    this.animalShelterService.refreshList$.subscribe(() => {
      this.animalShelterService.getAllAnimalShelter().then((resp) =>{
        this.shelters =[...resp]
      })
    });
  }

  selectUser(user: UserDetailDTO) {
    this.adminService.getPostCreatedByUser(user.id!).then((resp) => {
      this.searchTerm = user.name!;
      this.postList = [...resp];
    });
  }

  selectShelter(shelter: AnimalShelterDTO){
    this.animalShelterService.selectShelter(shelter);
  }

  isShelterRoute(): boolean {
    return this.router.url.includes('/admin/shelter-form');
  }
}
