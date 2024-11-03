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
import {
  FirebaseStorage,
  Storage,
  ref,
  uploadBytesResumable,
} from '@angular/fire/storage';
import { getDownloadURL } from '@firebase/storage';
import { BreakpointObserver, Breakpoints } from '@angular/cdk/layout';
import { HttpClientModule } from "@angular/common/http";
import { MatIconRegistry } from '@angular/material/icon';

@Component({
  selector: 'app-feed',
  standalone: true,
  templateUrl: './feed.component.html',
  styleUrl: './feed.component.css',
  imports: [ImportModule, FeedPostComponent, HttpClientModule],
})
export class FeedComponent implements OnInit {
  @Input() isAdminMode: boolean = false;
  @Input() posts: FeedPostResponseDTO[] = [];
  @Output() postRefresh = new EventEmitter<number>();
  categories: CategoryDto[] = [];
  postForm: UntypedFormGroup;
  isSmallScreen = false;
  isNewPostFormOpened = false;

  constructor(
    private sanitizer: DomSanitizer,
    private postService: FeedPostService,
    private storage: Storage,
    private breakpointObserver: BreakpointObserver,
    private matIconRegistry: MatIconRegistry
  ) {
    this.postForm = new UntypedFormGroup({
      title: new UntypedFormControl('', Validators.required),
      contentText: new UntypedFormControl('', Validators.required),
      category: new UntypedFormControl('', Validators.required),
      image: new UntypedFormControl(null),
    });
    postService.getCategories().then((resp) => {
      this.categories = resp;
      this.categories.forEach(cat =>{
        this.matIconRegistry.addSvgIcon(cat.assestIconHref!, this.sanitizer.bypassSecurityTrustResourceUrl(`assets/${cat.assestIconHref}`))
      })

    });
  }

  ngOnInit() {
    this.breakpointObserver.observe([Breakpoints.Handset]).subscribe(result => {
      this.isSmallScreen = result.matches;
    });
  }

  image: string | SafeUrl = '';
  currentLocation = 'Hely meghatározása';
  geocodedLocation: LocationDTO = {};
  updateImage(event: any) {
    const file = event.target.files[0];
    const storageRef = ref(this.storage, file.name);
    uploadBytesResumable(storageRef, file).then((val) =>
      getDownloadURL(val.ref).then((url) =>
        this.postForm.controls['image'].setValue(url)
      )
    );
    this.image = this.sanitizer.bypassSecurityTrustUrl(
      window.URL.createObjectURL(file)
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
        imageUrl: this.postForm.controls['image'].value,
      };
      this.postService.createPost(post).then((id) => {
        if (id) {
          this.postRefresh.emit(id);
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
              this.geocodedLocation.url = this.generateGoogleMapsUrl(pos.lat,pos.lng,resp.results[0].place_id)
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

  openForm(category: CategoryDto){
    const currentCategory = this.postForm.controls['category'].value;

    if (this.isNewPostFormOpened && currentCategory === category.id) {
      this.isNewPostFormOpened = false;
    } else {
      this.isNewPostFormOpened = true;
      this.postForm.controls['category'].patchValue(category.id);
    }
  }

  generateGoogleMapsUrl(placeLatitude: number, placeLongitude: number, placeId: string): string {
    const url = `https://www.google.com/maps/search/?api=1&query=${encodeURIComponent(placeLatitude)},${encodeURIComponent(placeLongitude)}&query_place_id=${encodeURIComponent(placeId)}`;
    return url;
  }
}
