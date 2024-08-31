import {
  AfterViewInit,
  Component,
  Input,
  OnInit,
  contentChild,
} from '@angular/core';
import { ImportModule } from '../../../../common/import.module';
import { Loader } from '@googlemaps/js-api-loader';
import { environment } from '../../../../../../environments/environment.development';
import { FeedPostResponseDTO } from '../../../../../../apiClient/data-contracts';
@Component({
  selector: 'app-map',
  standalone: true,
  imports: [ImportModule],
  templateUrl: './map.component.html',
  styleUrl: './map.component.css',
})
export class MapComponent implements OnInit {
  @Input() posts: FeedPostResponseDTO[] = [];
  options: google.maps.MapOptions = {
    mapId: 'dab6a2e41b45a87d',
    center: { lat: -31, lng: 147 },
    zoom: 13,
  };

  loader = new Loader({
    apiKey: environment.googleApiKey,
    version: 'weekly',
  });

  async ngOnInit() {
    if (navigator.geolocation) {
      await navigator.geolocation.getCurrentPosition(
        (position: GeolocationPosition) => {
          const pos = {
            lat: position.coords.latitude,
            lng: position.coords.longitude,
          };
          this.options.center = pos;
          this.loader.importLibrary('maps').then(() => this.initMap());
        },
        () => {}
      );
    }
  }
  map: google.maps.Map | undefined;

  async initMap() {
    // Request needed libraries.
    const { Map } = (await google.maps.importLibrary(
      'maps'
    )) as google.maps.MapsLibrary;
    const { AdvancedMarkerElement, PinElement } =
      (await google.maps.importLibrary('marker')) as google.maps.MarkerLibrary;

    this.map = new google.maps.Map(
      document.getElementById('map') as HTMLElement,
      {
        ...this.options,
      }
    );

    const icons: Record<string, { icon: string }> = {
      lost: {
        icon: '/assets/lost.svg',
      },
      found: {
        icon: '/assets/found.svg',
      },
      help: {
        icon: '/assets/help.svg',
      },
      ad: {
        icon: '/assets/ad.svg',
      },
    };

    const lostString =
      '<h2>Morzsi Elveszett</h2>' +
      '<p>XII Ker. Budapest Sáfrány utca 147/2</p>' +
      '<div style="text-align:center"><img src="/assets/dog.webp" class="img-fluid" alt="Sample image" width="150px"/></div>' +
      '<div style="display: flex; justify-content: space-between;">' +
      '<span class="material-icons">visibility</span>' +
      '<span class="material-icons">thumb_up</span>' +
      '<span class="material-icons">arrow_forward</span>' +
      '</div>';

    const helpString =
      '<h2>${title}</h2>' +
      '<p>${address}</p>' +
      '<div style="text-align:center"><img src="${imgUrl}" class="img-fluid" alt="Sample image" width="200" height="200" priority/></div>' +
      '<div style="display: flex; justify-content: space-between;">' +
      '<span class="material-icons">visibility</span>' +
      '<span class="material-icons">thumb_up</span>' +
      '<span class="material-icons">arrow_forward</span>' +
      '</div>';

    const foundString =
      '<h2>Talált süni</h2>' +
      '<p>XII Ker. Budapest Sáfrány utca 147/2</p>' +
      '<div style="text-align:center"><img src="/assets/hedgehog.avif" class="img-fluid" alt="Sample image" width="150px"/></div>' +
      '<div style="display: flex; justify-content: space-between;">' +
      '<span class="material-icons">visibility</span>' +
      '<span class="material-icons">thumb_up</span>' +
      '<span class="material-icons">arrow_forward</span>' +
      '</div>';

    const adString =
      '<h2>Örökbefogadható csöppségek</h2>' +
      '<p>XII Ker. Budapest Sáfrány utca 147/2</p>' +
      '<div style="text-align:center"><img src="/assets/babys.jpg" class="img-fluid" alt="Sample image" width="150px"/></div>' +
      '<div style="display: flex; justify-content: space-between;">' +
      '<span class="material-icons">visibility</span>' +
      '<span class="material-icons">thumb_up</span>' +
      '<span class="material-icons">arrow_forward</span>' +
      '</div>';

    var features = [
      {
        position: new google.maps.LatLng(47.476923, 19.1004811),
        type: 'lost',
        content: lostString,
      },
      {
        position: new google.maps.LatLng(47.486923, 19.1004811),
        type: 'help',
        content: helpString,
      },
      {
        position: new google.maps.LatLng(47.456923, 19.1004811),
        type: 'found',
        content: foundString,
      },
      {
        position: new google.maps.LatLng(47.466923, 19.1004811),
        type: 'ad',
        content: adString,
      },
    ];

    this.posts.forEach((post) => {
      features.push({
        position: new google.maps.LatLng(
          post.location?.latitude!,
          post.location?.longitude
        ),
        type: 'help',
        content: this.fillTemplateString(helpString, {
          title: post.title,
          address: post.location?.address,
          imgUrl: post.imageUrl,
        }),
      });
    });

    var lastOpenedWindow: google.maps.InfoWindow;
    for (let i = 0; i < features.length; i++) {
      const iconImage = document.createElement('img');
      iconImage.src = icons[features[i].type].icon;
      iconImage.width = 40;
      iconImage.height = 40;
      const marker = new google.maps.marker.AdvancedMarkerElement({
        map: this.map,
        position: features[i].position,
        content: iconImage,
      });
      const infowindow = new google.maps.InfoWindow({
        content: features[i].content,
      });
      marker.gmpClickable = true;
      marker.addListener('click', (e: any) => {
        if (lastOpenedWindow) lastOpenedWindow.close();
        infowindow.open(marker.map, marker);
        lastOpenedWindow = infowindow;
      });
    }
  }
  fillTemplateString(
    template: string,
    variables: { [key: string]: any }
  ): string {
    return template.replace(
      /\${(.*?)}/g,
      (match, p1) => variables[p1.trim()] || ''
    );
  }
}
