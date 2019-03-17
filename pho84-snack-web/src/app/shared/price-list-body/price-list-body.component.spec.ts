import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PriceListBodyComponent } from './price-list-body.component';

describe('PriceListBodyComponent', () => {
  let component: PriceListBodyComponent;
  let fixture: ComponentFixture<PriceListBodyComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PriceListBodyComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PriceListBodyComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
