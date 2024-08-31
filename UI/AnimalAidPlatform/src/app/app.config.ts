import { ApplicationConfig } from '@angular/core';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';
import {
  HTTP_INTERCEPTORS,
  provideHttpClient,
  withInterceptors,
  withInterceptorsFromDi,
} from '@angular/common/http';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { SampleInterceptor } from './core/services/tempIntercept';
import { initializeApp, provideFirebaseApp } from '@angular/fire/app';
import { getAnalytics, provideAnalytics, ScreenTrackingService } from '@angular/fire/analytics';
import { getStorage, provideStorage } from '@angular/fire/storage';

export const appConfig: ApplicationConfig = {
  providers: [
    provideRouter(routes),
    provideHttpClient(withInterceptorsFromDi()),
    { provide: HTTP_INTERCEPTORS, useClass: SampleInterceptor, multi: true },
    provideAnimationsAsync(),
    provideAnimationsAsync(), provideFirebaseApp(() => initializeApp({"projectId":"animalaidplatform","appId":"1:865356677096:web:b7966f24c66944cb70ae5d","storageBucket":"animalaidplatform.appspot.com","apiKey":"AIzaSyAslb2Vl1-cbr0rd6_9d2DOp0bMohVGtYo","authDomain":"animalaidplatform.firebaseapp.com","messagingSenderId":"865356677096","measurementId":"G-5PKS6T6FB2"})), provideAnalytics(() => getAnalytics()), ScreenTrackingService, provideStorage(() => getStorage()),
  ],
};
