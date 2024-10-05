import { Injectable } from '@angular/core';
import { Api } from '../../../apiClient/Api';
import { environment } from '../../../environments/environment.development';
import { ApiService } from './api.service';
import { LocationDTO, NotificationSettingsDto } from '../../../apiClient/data-contracts';

@Injectable({
  providedIn: 'root',
})
export class UserSettingsService {
  constructor(private apiService: ApiService) {}

  async getNotificationById(): Promise<NotificationSettingsDto> {
    return new Promise<NotificationSettingsDto>((resolve, reject) => {
      this.apiService.api.notificationSettingsList().catch((resp) => {
        if (resp.ok) {
          console.log(resp.data)
          resolve(resp.data);
        }else{
          var defaultLocation: LocationDTO ={
            latitude: 47.497913,
            longitude: 19.040236,
            address: "Budapest"
          }
          var defaultSettings: NotificationSettingsDto ={
            pushNotificationEnabled: false,
            location: defaultLocation,
            radius: 1,
            categoryIds: []
          }
          resolve(defaultSettings)
        }
      }).then((resp) =>{
        if(resp){
          resolve(resp.data)
        }
      }
      );
    });
  }

  async updateNotificationSettings(settings: NotificationSettingsDto) {
    return new Promise<boolean>((resolve, reject) => {
      this.apiService.api.notificationSettingsUpdate(settings).then((resp) => {
        resolve(resp.ok);
      });
    });
  }
}
