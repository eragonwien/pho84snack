import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CarouselFeatureComponent } from './carousel-feature.component';

describe('CarouselFeatureComponent', () => {
  let component: CarouselFeatureComponent;
  let fixture: ComponentFixture<CarouselFeatureComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CarouselFeatureComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CarouselFeatureComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
