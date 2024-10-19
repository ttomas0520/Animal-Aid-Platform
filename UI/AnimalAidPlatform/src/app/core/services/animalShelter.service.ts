import { Injectable } from "@angular/core";
import { ApiService } from "./api.service";
import { AnimalShelterDTO } from "../../../apiClient/data-contracts";
import { AdminService } from "./admin.service";
import { BehaviorSubject, Subject } from 'rxjs';

@Injectable({
    providedIn: 'root',
  })
  export class AnimalShelterService {
    constructor(private apiService: ApiService) {}

    private shelterSource = new BehaviorSubject<AnimalShelterDTO | null>(null);
    selectedShelter$ = this.shelterSource.asObservable();

    selectShelter(shelter: AnimalShelterDTO) {
      this.shelterSource.next(shelter);
    }

    private refreshShelterListSource = new Subject<void>();
    refreshList$ = this.refreshShelterListSource.asObservable();

    notifyRefreshShelterList() {
      this.refreshShelterListSource.next();
    }
    
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