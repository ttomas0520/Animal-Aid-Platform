import { inject, Injectable } from '@angular/core';
import { Api } from '../../../apiClient/Api';
import { environment } from '../../../environments/environment.development';
import { HttpClient } from '@angular/common/http';
import { AuthService } from './auth.service';
import { ApiService } from './api.service';
import {
  AnimalShelterDTO,
  FeedPostResponseDTO,
  UserDetailDTO,
} from '../../../apiClient/data-contracts';
import { user } from '@angular/fire/auth';

@Injectable({
  providedIn: 'root',
})
export class AdminService {
  constructor(private apiService: ApiService) {}

  async getRoles() {
    console.log(await this.apiService.api.rolesList());
    /*this.http
      .get('https://jsonplaceholder.typicode.com/users')
      .subscribe((res) => {
        console.log(res);
      });*/
  }

  async getUsers(): Promise<Array<UserDetailDTO>> {
    return new Promise<Array<UserDetailDTO>>((resolve, reject) => {
      this.apiService.api.userList().then((resp) => {
        if (resp.ok) {
          resolve(resp.data);
        }
      });
    });
  }

  async getPostCreatedByUser(
    userId: string
  ): Promise<Array<FeedPostResponseDTO>> {
    return new Promise<Array<FeedPostResponseDTO>>((resolve, reject) => {
      this.apiService.api.postUserDetail(userId).then((resp) => {
        if (resp.ok) {
          resolve(resp.data);
        }
      });
    });
  }

  async addAnimalShelter(dto: AnimalShelterDTO): Promise<boolean> {
    return new Promise<boolean>((resolve, reject) => {
      this.apiService.api.animalShelterCreate(dto).catch((resp) => {
        if (resp.ok) {
          resolve(resp.ok);
        }
      })
    });
  }

  async updateAnimalShelter(id: number, updatableShelter: AnimalShelterDTO): Promise<AnimalShelterDTO>{
    return new Promise<AnimalShelterDTO>((resolve,reject) =>{
      this.apiService.api.animalShelterUpdate(id, updatableShelter).then((resp) =>{
        if(resp){
          resolve(resp.data)
        }
      })
    })
  }
}
