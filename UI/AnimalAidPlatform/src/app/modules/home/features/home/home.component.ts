import { Component } from '@angular/core';
import { ImportModule } from '../../../common/import.module';
import { FeedComponent } from './feed/feed.component';
import { MapComponent } from './map/map.component';
import { Api } from '../../../../../apiClient/Api';
import { FeedPostService } from '../../../../core/services/feedPost.service';
import { FeedPostResponseDTO } from '../../../../../apiClient/data-contracts';

@Component({
  selector: 'app-home',
  standalone: true,
  templateUrl: './home.component.html',
  styleUrl: './home.component.css',
  imports: [ImportModule, FeedComponent, MapComponent],
})
export class HomeComponent {
  public postList: FeedPostResponseDTO[] = [];
  constructor(private postService: FeedPostService) {
    postService.getPosts().then((resp) => {
      this.postList = [...resp];
    });
  }

  refreshPostList(id: number) {
    this.postService.getPosts().then((resp) => {
      this.postList = [...resp];
    });
    if (
      this.postList.find((v) => {
        v.id == id;
      })
    ) {
      alert('Sikeres frissítés');
    }
  }
}
