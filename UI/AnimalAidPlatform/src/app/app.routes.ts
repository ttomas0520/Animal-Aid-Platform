import { Routes } from '@angular/router';
import { CategoryListComponent } from './features/category/category-list/category-list.component';
import { AppComponent } from './app.component';
import { AddCategoryComponent } from './features/category/add-category/add-category.component';
import { UserRegisterComponent } from './core/components/user-register/user-register.component';
import { UserLoginComponent } from './core/components/user-login/user-login.component';

export const routes: Routes = [
  {
    path: 'admin/categories',
    component: CategoryListComponent,
  },
  {
    path: 'admin/categories/add',
    component: AddCategoryComponent,
  },
  {
    path: 'user/register',
    component: UserRegisterComponent,
  },
  {
    path: 'user/login',
    component: UserLoginComponent,
  },
];
