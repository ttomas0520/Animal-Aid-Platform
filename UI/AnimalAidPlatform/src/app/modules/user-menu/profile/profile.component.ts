import { Component } from '@angular/core';
import { ImportModule } from '../../common/import.module';

@Component({
  selector: 'app-profile',
  standalone: true,
  imports: [ImportModule],
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.css',
})
export class ProfileComponent {}
