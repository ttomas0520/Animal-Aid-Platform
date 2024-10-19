import { Component, EventEmitter, Output } from '@angular/core';
import { FormGroup, FormBuilder, Validators, UntypedFormGroup } from '@angular/forms';
import { ImportModule } from '../../../../common/import.module';
import { AdminService } from '../../../../../core/services/admin.service';
import { LocationDTO } from '../../../../../../apiClient/data-contracts';
import { Loader } from '@googlemaps/js-api-loader';
import { environment } from '../../../../../../environments/environment.development';
import { AnimalShelterDTO } from "../../../../../../apiClient/data-contracts";
import { AnimalShelterService } from '../../../../../core/services/animalShelter.service';

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

  selectedShelter: AnimalShelterDTO | null = null;

  constructor(private fb: FormBuilder, 
    private adminService: AdminService,
    private shelterService: AnimalShelterService
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
    this.shelterService.selectedShelter$.subscribe((shelter) => {
      this.selectedShelter = shelter;
      if(this.selectedShelter != null){
        this.geocodedLocation = this.selectedShelter?.location!;
        this.registrationForm.patchValue({
          shelterInfo: {
            name: this.selectedShelter!.name,
            type: this.selectedShelter!.type,
            description: this.selectedShelter!.description || '' // Üres string ha null vagy undefined
          },
          locationInfo: {
            address: this.selectedShelter!.location?.address || '', // Feltételezve, hogy a LocationDTO tartalmazza az address-t
            phoneNumber: this.selectedShelter!.phoneNumber,
            email: this.selectedShelter!.email,
            website: this.selectedShelter!.website || ''
          },
          contactInfo: {
            contactName: this.selectedShelter!.contactName,
            contactPosition: this.selectedShelter!.contactPosition
          },
          openingHours: {
            weekdays: this.selectedShelter!.weekdays || '',
            weekend: this.selectedShelter!.weekend || ''
          },
          services: {
            adoption: this.selectedShelter!.adoption || false,
            visiting: this.selectedShelter!.visiting || false,
            volunteering: this.selectedShelter!.volunteering || false,
            medicalCare: this.selectedShelter!.medicalCare || false,
            donations: this.selectedShelter!.donations || false
          }
        });
      }
      });
    this.loader.importLibrary('maps').then(async () => {
      const { Autocomplete } = (await google.maps.importLibrary(
        'places'
      )) as google.maps.PlacesLibrary;

      const input = document.getElementById('autocomplete') as HTMLInputElement;
      this.autocomplete = new Autocomplete(input);
      this.autocomplete.addListener('place_changed', () => {
      const place = this.autocomplete?.getPlace();

        if (place && place.geometry && place.geometry.location) {
          this.geocodedLocation = {
            latitude: place.geometry.location.lat(),
            longitude: place.geometry.location.lng(),
            address: place.formatted_address,
          };
        }
      })
    });

  }


  onSubmit(): void {
    if (this.registrationForm!.valid) {
      console.log('Form Submitted', this.geocodedLocation);
      var animalShelter = this.getAnimalShelterDTO()
      console.log(this.adminService.addAnimalShelter(animalShelter))
    } else {
      console.log('Form is invalid');
    }
  }

  onUpdate(): void {
    if(this.registrationForm!.valid && this.selectedShelter != null){
      var animalShelter = this.getAnimalShelterDTO()
      animalShelter.id= this.selectedShelter.id
      this.adminService.updateAnimalShelter(this.selectedShelter?.id!, animalShelter).then(resp => this.shelterService.notifyRefreshShelterList())
    }
  }

  resetSelected() :void {
    if(this.selectedShelter != null){
      this.selectedShelter = null
    }
  }




   // Getter függvények a form csoportokhoz
   get shelterInfo() {
    return this.registrationForm.get('shelterInfo') as FormGroup;
  }

  get locationInfo() {
    return this.registrationForm.get('locationInfo') as FormGroup;
  }

  get contactInfo() {
    return this.registrationForm.get('contactInfo') as FormGroup;
  }

  get openingHours() {
    return this.registrationForm.get('openingHours') as FormGroup;
  }

  get services() {
    return this.registrationForm.get('services') as FormGroup;
  }

  // Getter függvények az egyes mezőkhöz
  get name() {
    return this.shelterInfo.get('name');
  }

  get type() {
    return this.shelterInfo.get('type');
  }

  get description() {
    return this.shelterInfo.get('description');
  }

  get address() {
    return this.locationInfo.get('address');
  }

  get phoneNumber() {
    return this.locationInfo.get('phoneNumber');
  }

  get email() {
    return this.locationInfo.get('email');
  }

  get website() {
    return this.locationInfo.get('website');
  }

  get contactName() {
    return this.contactInfo.get('contactName');
  }

  get contactPosition() {
    return this.contactInfo.get('contactPosition');
  }

  get weekdays() {
    return this.openingHours.get('weekdays');
  }

  get weekend() {
    return this.openingHours.get('weekend');
  }

  get adoption() {
    return this.services.get('adoption');
  }

  get visiting() {
    return this.services.get('visiting');
  }

  get volunteering() {
    return this.services.get('volunteering');
  }

  get medicalCare() {
    return this.services.get('medicalCare');
  }

  get donations() {
    return this.services.get('donations');
  }

  getAnimalShelterDTO(): AnimalShelterDTO {
    return {
      id: this.registrationForm.get('id')?.value,
      name: this.shelterInfo.get('name')?.value,
      type: this.shelterInfo.get('type')?.value,
      description: this.shelterInfo.get('description')?.value,
      location: {
        address: this.geocodedLocation.address,
        longitude: this.geocodedLocation.longitude,
        latitude: this.geocodedLocation.latitude 
      },
      phoneNumber: this.locationInfo.get('phoneNumber')?.value,
      email: this.locationInfo.get('email')?.value,
      website: this.locationInfo.get('website')?.value,
      contactName: this.contactInfo.get('contactName')?.value,
      contactPosition: this.contactInfo.get('contactPosition')?.value,
      weekdays: this.openingHours.get('weekdays')?.value,
      weekend: this.openingHours.get('weekend')?.value,
      adoption: this.services.get('adoption')?.value,
      visiting: this.services.get('visiting')?.value,
      volunteering: this.services.get('volunteering')?.value,
      medicalCare: this.services.get('medicalCare')?.value,
      donations: this.services.get('donations')?.value
    };
  }
}
