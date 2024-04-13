import { Component, OnInit } from '@angular/core';
import 'ol/ol.css';
import Map from 'ol/Map';
import View from 'ol/View';
import { OSM, Vector as VectorSource } from 'ol/source.js';
import { Tile as TileLayer, Vector as VectorLayer } from 'ol/layer';
import Geolocation from 'ol/Geolocation';
import { Feature } from 'ol';
import { Circle as CircleStyle, Fill, Stroke, Style } from 'ol/style';
import { Circle, Geometry, Point } from 'ol/geom';
import { transform } from 'ol/proj';
@Component({
  selector: 'app-map',
  standalone: true,
  imports: [],
  templateUrl: './map.component.html',
  styleUrl: './map.component.css',
})
export class MapComponent {
  map = new Map();

  ngAfterViewInit(): void {
    console.log('init');
    this.map = new Map({
      view: new View({
        center: [0, 0],
        zoom: 1,
      }),
      layers: [
        new TileLayer({
          source: new OSM(),
        }),
      ],
      target: 'ol-map',
    });

    const view = new View({
      center: [0, 0],
      zoom: 2,
    });

    const geolocation = new Geolocation({
      // enableHighAccuracy must be set to true to have the heading value.
      trackingOptions: {
        enableHighAccuracy: true,
      },
      projection: view.getProjection(),
    });

    const positionFeature = new Feature();
    positionFeature.setStyle(
      new Style({
        image: new CircleStyle({
          radius: 6,
          fill: new Fill({
            color: '#3399CC',
          }),
          stroke: new Stroke({
            color: '#fff',
            width: 2,
          }),
        }),
      })
    );

    geolocation.on('change:position', function () {
      const coordinates = geolocation.getPosition();
      positionFeature.setGeometry(
        coordinates ? (new Point(coordinates) as Geometry) : undefined
      );
    });

    const accuracyFeature = new Feature();
    geolocation.on('change:accuracyGeometry', function () {
      accuracyFeature.setGeometry(
        geolocation.getAccuracyGeometry() as Geometry
      );
    });

    geolocation.setTracking(true);

    var circle = new Circle(
      transform([2.1833, 41.3833], 'EPSG:4326', 'EPSG:3857'),
      10
      
    );

    var circleFeature = new Feature(circle);
    new VectorLayer({
      map: this.map,
      source: new VectorSource({
        features: [accuracyFeature, positionFeature, circleFeature],
      }),
    });
  }
}
