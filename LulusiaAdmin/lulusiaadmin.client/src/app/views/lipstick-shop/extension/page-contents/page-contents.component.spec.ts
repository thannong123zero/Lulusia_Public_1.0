import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PageContentsComponent } from './page-contents.component';

describe('PageContentsComponent', () => {
  let component: PageContentsComponent;
  let fixture: ComponentFixture<PageContentsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PageContentsComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PageContentsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
