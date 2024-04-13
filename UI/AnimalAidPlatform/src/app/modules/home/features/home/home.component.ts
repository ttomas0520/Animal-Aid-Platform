import { Component } from '@angular/core';
import { ImportModule } from '../../../common/import.module';
import { FeedComponent } from "./feed/feed.component";
import { MapComponent } from "./map/map.component";

@Component({
    selector: 'app-home',
    standalone: true,
    templateUrl: './home.component.html',
    styleUrl: './home.component.css',
    imports: [ImportModule, FeedComponent, MapComponent]
})
export class HomeComponent {

}
