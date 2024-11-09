import { Component, ElementRef, HostListener, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterLink, RouterLinkActive, RouterOutlet } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { MatMenuModule } from '@angular/material/menu';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { ImportModule } from '../../../modules/common/import.module';
import { AnimalShelterDTO, FeedPostResponseDTO, UserDetailDTO } from '../../../../apiClient/data-contracts';
import { FeedPostService } from '../../services/feedPost.service';
import { AnimalShelterService } from '../../services/animalShelter.service';
@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [ImportModule],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css',
})
export class NavbarComponent {
  authService = inject(AuthService);
  name: string = '';

  showResults = false;
  searchTerm = '';
  shelters: AnimalShelterDTO[] =[];
  posts: FeedPostResponseDTO[] =[];
  filteredShelters: AnimalShelterDTO[] = [];
  filteredPosts: FeedPostResponseDTO[] = [];

  constructor(private postService: FeedPostService, private animalShelterService: AnimalShelterService,private eRef: ElementRef, private router: Router ) {
    this.postService.getPosts().then(resp => this.posts=[...resp])
    this.animalShelterService.getAllAnimalShelter().then(resp => this.shelters = [...resp])
  }

  checkAuthenticated(): boolean {
    if (this.authService.isAuthenticated()) {
      this.name = this.authService.getUserName()!;
      return true;
    }
    return false;
  }

  filterResults() {
    const term = this.searchTerm.toLowerCase();
    if(term == ''){
      this.filteredShelters = this.filteredPosts = []
    }
    this.filteredShelters = this.shelters
      .filter(shelter =>
        [shelter.name, shelter.type, shelter.description, shelter.contactName]
          .some(field => field?.toLowerCase().includes(term))
      )
      .slice(0, 3);

    this.filteredPosts = this.posts
      .filter(post =>
        [post.title, post.contentText, post.creatorName]
          .some(field => field?.toLowerCase().includes(term))
      )
      .slice(0, 3);
  }

  onFocus() {
    this.showResults = true;
  }

  @HostListener('document:click', ['$event'])
  clickOutside(event: MouseEvent) {
    if (!this.eRef.nativeElement.contains(event.target)) {
      this.showResults = false;
    }
  }

  navigateToPost(id: number): void {
    this.router.navigate(['/detailed-view/post', id]);
    this.showResults = false;
  }

  navigateToShelter(id: number): void {
    this.router.navigate(['/detailed-view/shelter', id]);
    this.showResults = false;
  }

}
