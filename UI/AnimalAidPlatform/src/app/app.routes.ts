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

export const routes: Routes = [
  {
    path: '',
    redirectTo: '/home',
    pathMatch: 'full',
  },
  {
    path: 'admin/categories',
    component: CategoryListComponent,
    canActivate: [authGuard, adminGuard],
  },
  {
    path: 'admin/categories/add',
    component: AddCategoryComponent,
    canActivate: [authGuard, adminGuard],
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
];
