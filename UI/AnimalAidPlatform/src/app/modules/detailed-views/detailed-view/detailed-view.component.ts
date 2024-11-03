import { Component } from '@angular/core';
import { ImportModule } from '../../common/import.module';
import { ActivatedRoute } from '@angular/router';
import { FeedPostService } from '../../../core/services/feedPost.service';
import { AnimalShelterService } from '../../../core/services/animalShelter.service';
import { AnimalShelterDTO, FeedPostResponseDTO } from '../../../../apiClient/data-contracts';
import { FeedPostComponent } from "../../common/feed-post/feed-post.component";

@Component({
  selector: 'app-detailed-view',
  standalone: true,
  imports: [ImportModule, FeedPostComponent],
  templateUrl: './detailed-view.component.html',
  styleUrl: './detailed-view.component.css'
})
export class DetailedViewComponent {
  id: string | null = null;
  isPostView: boolean = false; 
  feedPost?: FeedPostResponseDTO;
  animalShelter?: AnimalShelterDTO;
  isLoading: boolean = true;

  constructor(
    private route: ActivatedRoute,
    private postService: FeedPostService,
    private shelterService: AnimalShelterService,
  ) {}

  ngOnInit(): void {
    this.route.paramMap.subscribe(async params => {
      this.id = params.get('id');
      this.isLoading = true; 

      const parentPath = this.route.snapshot.url[0]?.path;
      
      if (parentPath === 'post') {
        this.isPostView = true; 
        await this.postService.getPostById(Number(this.id)).then(resp => this.feedPost = resp);
      } else if (parentPath === 'shelter') {
        this.isPostView = false; 
        await this.shelterService.getAnimalShelterById(Number(this.id)).then(resp => this.animalShelter = resp);
      }

      this.isLoading = false; 
      console.log(`Navigált ID: ${this.id}`);
      console.log(`Aktuális nézet: ${this.isPostView ? 'Post :' + this.feedPost : 'Shelter :' + this.animalShelter}`);
    });
  }
}