import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ImportModule } from '../../../../common/import.module';
import { DomSanitizer, SafeUrl } from '@angular/platform-browser';

@Component({
  selector: 'app-feed',
  standalone: true,
  imports: [ImportModule],
  templateUrl: './feed.component.html',
  styleUrl: './feed.component.css',
})
export class FeedComponent {
  constructor(private sanitizer: DomSanitizer) {}
  image: string | SafeUrl = '';

  updateImage(ev: any) {
    this.image = this.sanitizer.bypassSecurityTrustUrl(
      window.URL.createObjectURL(ev.target.files[0])
    );
  }
}
