import { Injectable } from '@angular/core';
import { Api } from '../../../apiClient/Api';
import { environment } from '../../../environments/environment.development';
import { ApiService } from './api.service';
import { NotificationSettingsDto } from '../../../apiClient/data-contracts';

@Injectable({
  providedIn: 'root',
})
export class UserSettingsService {
  constructor(private apiService: ApiService) {}

  async getNotificationById(): Promise<NotificationSettingsDto> {
    return new Promise<NotificationSettingsDto>((resolve, reject) => {
      this.apiService.api.notificationSettingsList().then((resp) => {
        if (resp.ok) {
          resolve(resp.data);
        }
      });
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
