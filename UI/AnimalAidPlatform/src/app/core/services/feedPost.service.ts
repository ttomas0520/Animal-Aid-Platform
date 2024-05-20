import { Injectable } from '@angular/core';
import { ApiService } from './api.service';
import {
  CategoryDto,
  CreatePostDTO,
  FeedPost,
  FeedPostResponseDTO,
} from '../../../apiClient/data-contracts';

@Injectable({
  providedIn: 'root',
})
export class FeedPostService {
  constructor(private apiService: ApiService) {}

  getPosts(): Promise<Array<FeedPostResponseDTO>> {
    return new Promise<Array<FeedPostResponseDTO>>((resolve, reject) => {
      this.apiService.api.postList().then((resp) => {
        if (resp.ok) {
          resolve(resp.data);
        } else {
          reject();
        }
      });
    });

    /*this.http
        .get('https://jsonplaceholder.typicode.com/users')
        .subscribe((res) => {
          console.log(res);
        });*/
  }

  createPost(post: CreatePostDTO): Promise<number> {
    return new Promise<number>((resolve, reject) => {
      this.apiService.api.postCreate(post).then((resp) => {
        if (resp.ok) {
          resolve(resp.data);
        } else {
          reject();
        }
      });
    });
  }

  getCategories(): Promise<Array<CategoryDto>> {
    return new Promise<Array<CategoryDto>>((resolve, reject) => {
      this.apiService.api.categoriesList().then((resp) => {
        if (resp.ok) {
          resolve(resp.data);
        }
      });
    });
  }
}
