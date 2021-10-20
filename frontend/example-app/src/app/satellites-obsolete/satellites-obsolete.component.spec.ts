import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SatellitesObsoleteComponent } from './satellites-obsolete.component';

describe('SatellitesObsoleteComponent', () => {
  let component: SatellitesObsoleteComponent;
  let fixture: ComponentFixture<SatellitesObsoleteComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SatellitesObsoleteComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SatellitesObsoleteComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
