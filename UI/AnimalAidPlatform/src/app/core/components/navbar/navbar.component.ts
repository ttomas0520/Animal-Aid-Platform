import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink, RouterLinkActive, RouterOutlet } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { MatMenuModule } from '@angular/material/menu';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { ImportModule } from '../../../modules/common/import.module';
@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [ImportModule],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css',
})
export class NavbarComponent {
  authService = inject(AuthService);
  name: string = '';

  constructor() {}

  ngOnInit() {
    console.log(this.name);
  }

  checkAuthenticated(): boolean {
    if (this.authService.isAuthenticated()) {
      this.name = this.authService.getUserName()!;
      return true;
    }
    return false;
  }
}
