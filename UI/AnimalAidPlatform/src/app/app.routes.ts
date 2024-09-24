import { Routes } from '@angular/router';
import { CategoryListComponent } from './modules/home/features/category/category-list/category-list.component';
import { AppComponent } from './app.component';
import { AddCategoryComponent } from './modules/home/features/category/add-category/add-category.component';
import { UserRegisterComponent } from './core/components/user-register/user-register.component';
import { UserLoginComponent } from './core/components/user-login/user-login.component';
import { FeedComponent } from './modules/home/features/home/feed/feed.component';
import { ErrorComponent } from './core/components/error/error.component';
import { adminGuard, authGuard } from './guards/guards.guard';
import { HomeComponent } from './modules/home/features/home/home.component';
import { AdminMenuComponent } from './modules/home/features/admin-menu/admin-menu.component';
import { NotificationSettingsComponent } from './modules/user-menu/notification-settings/notification-settings.component';
import { OwnedAnimalsComponent } from './modules/user-menu/owned-animals/owned-animals.component';
import { ProfileComponent } from './modules/user-menu/profile/profile.component';

export const routes: Routes = [
  {
    path: '',
    redirectTo: '/home',
    pathMatch: 'full',
  },
  {
    path: 'admin',
    component: AdminMenuComponent,
    canActivate: [authGuard, adminGuard],
    children: [
      {
        path: 'categories',
        component: CategoryListComponent,
        canActivate: [authGuard, adminGuard],
      },
      {
        path: 'admin/categories/add',
        component: AddCategoryComponent,
        canActivate: [authGuard, adminGuard],
      },
    ],
  },
  {
    path: 'user/register',
    component: UserRegisterComponent,
  },
  {
    path: 'user/login',
    component: UserLoginComponent,
  },
  {
    path: 'home',
    component: HomeComponent,
    canActivate: [authGuard],
  },
  {
    path: 'error',
    component: ErrorComponent,
  },
  {
    path: 'profile',
    canActivate: [authGuard],
    component: ProfileComponent,
    children: [
      {
        path: 'notification-settings',
        component: NotificationSettingsComponent,
        canActivate: [authGuard],
      },
      {
        path: 'owned-animals',
        component: OwnedAnimalsComponent,
        canActivate: [authGuard],
      },
    ],
  },
];
