import { Component, Input } from '@angular/core';
import { ImportModule } from '../import.module';
import { FeedPostResponseDTO } from '../../../../apiClient/data-contracts';

@Component({
  selector: 'app-feed-post',
  standalone: true,
  imports: [ImportModule],
  templateUrl: './feed-post.component.html',
  styleUrl: './feed-post.component.css',
})
export class FeedPostComponent {
  @Input() post?: FeedPostResponseDTO;
  @Input() isAdminMode: boolean = false;
}
