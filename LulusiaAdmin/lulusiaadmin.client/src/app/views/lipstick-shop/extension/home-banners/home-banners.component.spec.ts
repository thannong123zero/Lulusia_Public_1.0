import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HomeBannersComponent } from './home-banners.component';

describe('HomeBannersComponent', () => {
  let component: HomeBannersComponent;
  let fixture: ComponentFixture<HomeBannersComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [HomeBannersComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(HomeBannersComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
