import { Component, Input } from '@angular/core';
import { ImportModule } from '../import.module';
import { FeedPostResponseDTO } from '../../../../apiClient/data-contracts';
import { NgOptimizedImage } from '@angular/common';
import { FeedPostService } from '../../../core/services/feedPost.service';

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
  constructor(protected feedpostService: FeedPostService){}

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
}
