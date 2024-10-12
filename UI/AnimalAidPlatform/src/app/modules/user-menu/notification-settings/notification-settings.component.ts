import { Component } from '@angular/core';
import { ImportModule } from '../../common/import.module';
import {
  AbstractControl,
  FormArray,
  FormGroup,
  UntypedFormArray,
  UntypedFormBuilder,
  UntypedFormControl,
  UntypedFormGroup,
} from '@angular/forms';
import { FeedPostService } from '../../../core/services/feedPost.service';
import {
  CategoryDto,
  LocationDTO,
  NotificationSettingsDto,
} from '../../../../apiClient/data-contracts';
import { Loader } from '@googlemaps/js-api-loader';
import { environment } from '../../../../environments/environment.development';
import { MapComponent } from '../../home/features/home/map/map.component';
import { UserSettingsService } from '../../../core/services/userSettings.service';

@Component({
  selector: 'app-notification-settings',
  standalone: true,
  imports: [ImportModule, MapComponent],
  templateUrl: './notification-settings.component.html',
  styleUrl: './notification-settings.component.css',
})
export class NotificationSettingsComponent {
  notiForm: UntypedFormGroup;
  autocomplete: google.maps.places.Autocomplete | undefined;
  geocodedLocation: LocationDTO = {};
  settings: NotificationSettingsDto = {};

  loader = new Loader({
    apiKey: environment.googleApiKey,
    version: 'weekly',
  });

  constructor(
    private postService: FeedPostService,
    private userSettingsService: UserSettingsService,
    private fb: UntypedFormBuilder
  ) {
    this.notiForm = this.fb.group({
      pushNotificationChecked: [false],
      location: [''],
      radius: [1],
      categories: this.fb.array([]),
    });
   
  }


  async loadSettingsAndCategories() {
    try {
      // Párhuzamosan futtatjuk a két API hívást
      const [settings, categories] = await Promise.all([
        this.userSettingsService.getNotificationById(),
        this.postService.getCategories(),
      ]);

      this.settings = settings; 
      const categoryArray = categories.map((c) =>
        this.fb.group({
          id: [c.id],
          cName: [c.name],
          enabled: [this.settings.categoryIds!.includes(c.id!)],
        })
      );
      this.categories.clear();
      categoryArray.forEach((group) => this.categories.push(group));
    } catch (error) {
      console.error('Hiba történt az adatok betöltése során:', error);
    }
  }

  get categories(): UntypedFormArray {
    return this.notiForm.get('categories') as UntypedFormArray;
  }

  get radius(): number {
    return this.notiForm.get('radius')?.value as number;
  }

  cityCircle: google.maps.Circle | undefined;

  async ngOnInit() {
    await this.loadSettingsAndCategories().then(() => {
      if (this.settings != null) {
        this.notiForm.controls['pushNotificationChecked'].patchValue(
          this.settings.pushNotificationEnabled
        );
        this.notiForm.controls['radius'].patchValue(this.settings.radius);
        this.geocodedLocation = this.settings.location!;
      }
    });
    this.loader.importLibrary('maps').then(async () => {
      const map = new google.maps.Map(
        document.getElementById('map') as HTMLElement,
        {
          center: { lat: 47.497913, lng: 19.040236 },
          zoom: 13,
          mapTypeControl: false,
        }
      );

      this.cityCircle = new google.maps.Circle({
        strokeColor: '#FF0000',
        strokeOpacity: 0.8,
        strokeWeight: 2,
        fillColor: '#7f6a93',
        fillOpacity: 0.35,
      });

      const marker = new google.maps.Marker({
        map,
        anchorPoint: new google.maps.Point(0, -29),
      });

      if (this.settings != null) {
        this.cityCircle.setCenter(
          new google.maps.LatLng(
            this.settings.location?.latitude!,
            this.settings.location?.longitude
          )
        );
        this.cityCircle.setRadius(this.settings.radius! * 1000);
        this.cityCircle.setMap(map);
        map.fitBounds(this.cityCircle.getBounds()!);
        marker.setVisible(true);
        marker.setPosition(
          new google.maps.LatLng(
            this.settings.location?.latitude!,
            this.settings.location?.longitude
          )
        );
      }

      const { Autocomplete } = (await google.maps.importLibrary(
        'places'
      )) as google.maps.PlacesLibrary;

      const input = document.getElementById('autocomplete') as HTMLInputElement;
      this.autocomplete = new Autocomplete(input);
      this.notiForm.get('radius')?.valueChanges.subscribe((value) => {
        console.log(value);
        this.cityCircle?.setRadius(value * 1000);
        map.fitBounds(this.cityCircle?.getBounds()!);
      });
      // Add event listener for place changes
      this.autocomplete.addListener('place_changed', () => {
        const place = this.autocomplete?.getPlace();

        if (place && place.geometry && place.geometry.location) {
          if (place.geometry.viewport) {
            map.fitBounds(place.geometry.viewport);
          } else {
            map.setCenter(place.geometry.location);
            map.setZoom(17);
          }
          marker.setPosition(place.geometry.location);
          marker.setVisible(true);

          this.geocodedLocation = {
            latitude: place.geometry.location.lat(),
            longitude: place.geometry.location.lng(),
            address: place.formatted_address,
          };
          this.cityCircle!.setMap(map);
          this.cityCircle!.setCenter(place.geometry.location);

          map.fitBounds(this.cityCircle!.getBounds()!);
        }
      });
    });
  }

  onSubmit() {
    const categoryIds = (
      this.notiForm.controls['categories'] as UntypedFormArray
    ).controls
      .filter(
        (c: AbstractControl) =>
          (c as FormGroup).controls['enabled'].value === true
      )
      .map((c: AbstractControl) => (c as FormGroup).controls['id'].value);

    var settings: NotificationSettingsDto = {
      pushNotificationEnabled:
        this.notiForm.controls['pushNotificationChecked'].value,
      radius: this.notiForm.controls['radius'].value,
      categoryIds: categoryIds,
      location: this.geocodedLocation,
    };

    this.userSettingsService
      .updateNotificationSettings(settings)
      .then((resp) => console.log(resp));
  }
}
