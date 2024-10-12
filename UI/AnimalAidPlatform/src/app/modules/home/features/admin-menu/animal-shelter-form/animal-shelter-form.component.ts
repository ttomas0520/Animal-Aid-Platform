import { Component } from '@angular/core';
import { FormGroup, FormBuilder, Validators, UntypedFormGroup } from '@angular/forms';
import { ImportModule } from '../../../../common/import.module';
import { AdminService } from '../../../../../core/services/admin.service';
import { LocationDTO } from '../../../../../../apiClient/data-contracts';
import { Loader } from '@googlemaps/js-api-loader';
import { environment } from '../../../../../../environments/environment.development';

@Component({
  selector: 'app-animal-shelter-form',
  standalone: true,
  imports: [ImportModule],
  templateUrl: './animal-shelter-form.component.html',
  styleUrl: './animal-shelter-form.component.css'
})
export class AnimalShelterFormComponent {
  registrationForm: UntypedFormGroup;
  autocomplete: google.maps.places.Autocomplete | undefined;
  geocodedLocation: LocationDTO = {};

  loader = new Loader({
    apiKey: environment.googleApiKey,
    version: 'weekly',
  });

  constructor(private fb: FormBuilder, 
    private adminService: AdminService,
  ) {
    this.registrationForm = this.fb.group({
      shelterInfo: this.fb.group({
        name: ['', Validators.required],
        type: ['', Validators.required],
        description: ['']
      }),
      locationInfo: this.fb.group({
        address: ['', Validators.required],
        phoneNumber: ['', [Validators.required, Validators.pattern(/^[\d\+\-\.\(\)\/\s]*$/)]],
        email: ['', [Validators.required, Validators.email]],
        website: ['']
      }),
      contactInfo: this.fb.group({
        contactName: ['', Validators.required],
        contactPosition: ['', Validators.required]
      }),
      openingHours: this.fb.group({
        weekdays: [''],
        weekend: ['']
      }),
      services: this.fb.group({
        adoption: [false],
        visiting: [false],
        volunteering: [false],
        medicalCare: [false],
        donations: [false]
      })
    });
  }

  async ngOnInit(){
    this.loader.importLibrary('maps').then(async () => {
      const { Autocomplete } = (await google.maps.importLibrary(
        'places'
      )) as google.maps.PlacesLibrary;

      const input = document.getElementById('autocomplete') as HTMLInputElement;
      this.autocomplete = new Autocomplete(input);
      const place = this.autocomplete?.getPlace();

        if (place && place.geometry && place.geometry.location) {
          this.geocodedLocation = {
            latitude: place.geometry.location.lat(),
            longitude: place.geometry.location.lng(),
            address: place.formatted_address,
          };
        }
    });

  }


  onSubmit(): void {
    if (this.registrationForm!.valid) {
      console.log('Form Submitted', this.registrationForm!.value);

      //console.log(this.adminService.addAnimalShelter())
    } else {
      console.log('Form is invalid');
    }
  }
}
