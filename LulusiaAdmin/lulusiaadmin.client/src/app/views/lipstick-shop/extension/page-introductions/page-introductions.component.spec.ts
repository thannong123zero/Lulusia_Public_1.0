import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PageIntroductionsComponent } from './page-introductions.component';

describe('PageIntroductionsComponent', () => {
  let component: PageIntroductionsComponent;
  let fixture: ComponentFixture<PageIntroductionsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PageIntroductionsComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PageIntroductionsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
