import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OwnedAnimalsComponent } from './owned-animals.component';

describe('OwnedAnimalsComponent', () => {
  let component: OwnedAnimalsComponent;
  let fixture: ComponentFixture<OwnedAnimalsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [OwnedAnimalsComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(OwnedAnimalsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
