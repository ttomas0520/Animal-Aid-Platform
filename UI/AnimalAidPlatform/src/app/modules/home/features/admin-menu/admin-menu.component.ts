import { Component } from '@angular/core';
import { FeedComponent } from '../home/feed/feed.component';
import { ImportModule } from '../../../common/import.module';

@Component({
  selector: 'app-admin-menu',
  standalone: true,
  templateUrl: './admin-menu.component.html',
  styleUrl: './admin-menu.component.css',
  imports: [FeedComponent, ImportModule],
})
export class AdminMenuComponent {}
