import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ImportModule } from '../../../../common/import.module';
import { DomSanitizer, SafeUrl } from '@angular/platform-browser';
import { FeedPostComponent } from '../../../../common/feed-post/feed-post.component';
import { Loader } from '@googlemaps/js-api-loader';
import {
  CategoryDto,
  CreatePostDTO,
  FeedPostResponseDTO,
  LocationDTO,
} from '../../../../../../apiClient/data-contracts';
import { FeedPostService } from '../../../../../core/services/feedPost.service';
import {
  UntypedFormControl,
  UntypedFormGroup,
  Validators,
} from '@angular/forms';
import { environment } from '../../../../../../environments/environment.development';

@Component({
  selector: 'app-feed',
  standalone: true,
  templateUrl: './feed.component.html',
  styleUrl: './feed.component.css',
  imports: [ImportModule, FeedPostComponent],
})
export class FeedComponent implements OnInit {
  @Input() isAdminMode: boolean = false;
  @Input() posts: FeedPostResponseDTO[] = [];
  @Output() postCreated = new EventEmitter<number>();
  categories: CategoryDto[] = [];
  postForm: UntypedFormGroup;
  constructor(
    private sanitizer: DomSanitizer,
    private postService: FeedPostService
  ) {
    this.postForm = new UntypedFormGroup({
      title: new UntypedFormControl('', Validators.required),
      contentText: new UntypedFormControl('', Validators.required),
      category: new UntypedFormControl('', Validators.required),
      image: new UntypedFormControl(null),
    });
    postService.getCategories().then((resp) => (this.categories = resp));
  }

  ngOnInit() {
    console.log(this.posts);
  }
  image: string | SafeUrl = '';
  currentLocation = 'Hely meghatározása';
  geocodedLocation: LocationDTO = {};
  updateImage(ev: any) {
    this.image = this.sanitizer.bypassSecurityTrustUrl(
      window.URL.createObjectURL(ev.target.files[0])
    );
  }

  loader = new Loader({
    apiKey: environment.googleApiKey,
    version: 'weekly',
  });

  onSubmit() {
    if (this.postForm.valid) {
      var post: CreatePostDTO = {
        title: this.postForm.controls['title'].value,
        contentText: this.postForm.controls['contentText'].value,
        categoryId: this.postForm.controls['category'].value,
        location: this.geocodedLocation,
      };
      this.postService.createPost(post).then((id) => {
        if (id) {
          this.postCreated.emit(id);
        }
      });
    } else {
      alert('Form nem valid');
    }
  }

  async reverseGeocode() {
    this.loader.importLibrary('maps').then(async () => {
      const geocoder = new google.maps.Geocoder();
      if (navigator.geolocation) {
        await navigator.geolocation.getCurrentPosition(
          (position: GeolocationPosition) => {
            const pos = {
              lat: position.coords.latitude,
              lng: position.coords.longitude,
            };
            this.geocodedLocation.latitude = pos.lat;
            this.geocodedLocation.longitude = pos.lng;
            geocoder.geocode({ location: pos }).then((resp) => {
              this.currentLocation = resp.results[0].formatted_address;
              this.geocodedLocation.address = this.currentLocation;
              this.currentLocation = this.currentLocation.replace(
                ' Magyarország',
                ''
              );
            });
          },
          () => {}
        );
      }
    });
  }
}
