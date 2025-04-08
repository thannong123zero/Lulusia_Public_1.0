import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PageTypesComponent } from './page-types.component';

describe('PageTypesComponent', () => {
  let component: PageTypesComponent;
  let fixture: ComponentFixture<PageTypesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PageTypesComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PageTypesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
