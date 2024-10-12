import { Injectable } from "@angular/core";
import { ApiService } from "./api.service";
import { AnimalShelterDTO } from "../../../apiClient/data-contracts";

@Injectable({
    providedIn: 'root',
  })
  export class AnimalShelterService {
    constructor(private apiService: ApiService) {}
  
    async getAllAnimalShelter(): Promise<AnimalShelterDTO[]> {
      return new Promise<AnimalShelterDTO[]>((resolve, reject) => {
        this.apiService.api.animalShelterList().then((resp) =>{
          if(resp){
            resolve(resp.data)
          }
        }
        );
      });
    }
  }