import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ImportModule } from '../../../../common/import.module';

@Component({
  selector: 'app-feed',
  standalone: true,
  imports: [ImportModule],
  templateUrl: './feed.component.html',
  styleUrl: './feed.component.css',
})
export class FeedComponent {}
