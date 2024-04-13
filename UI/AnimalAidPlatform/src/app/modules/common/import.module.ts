import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import {
  RouterOutlet,
  RouterLink,
  RouterLinkActive,
  RouterModule,
} from '@angular/router';
import { MatButtonModule } from '@angular/material/button';
import { MatIcon, MatIconModule } from '@angular/material/icon';
import { MatMenuModule } from '@angular/material/menu';
import { MatInputModule } from '@angular/material/input';
import { MatOption } from '@angular/material/core';
import { MatSelectModule } from '@angular/material/select';
import { MatTabsModule } from '@angular/material/tabs';
@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    RouterOutlet,
    RouterLink,
    RouterLinkActive,
    FormsModule,
    RouterLink,
    RouterLinkActive,
    ReactiveFormsModule,
    RouterModule,

    //Material
    MatMenuModule,
    MatIconModule,
    MatButtonModule,
    MatInputModule,
    MatIcon,
    MatOption,
    MatSelectModule,
    MatTabsModule,
    //----//
  ],
  exports:[
    CommonModule,
    RouterOutlet,
    RouterLink,
    RouterLinkActive,
    FormsModule,
    RouterLink,
    RouterLinkActive,
    ReactiveFormsModule,
    RouterModule,

    //Material
    MatMenuModule,
    MatIconModule,
    MatButtonModule,
    MatInputModule,
    MatIcon,
    MatOption,
    MatSelectModule,
    MatTabsModule,
  ]
})
export class ImportModule {}
