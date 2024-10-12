import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AnimalShelterFormComponent } from './animal-shelter-form.component';

describe('AnimalShelterFormComponent', () => {
  let component: AnimalShelterFormComponent;
  let fixture: ComponentFixture<AnimalShelterFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AnimalShelterFormComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AnimalShelterFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
