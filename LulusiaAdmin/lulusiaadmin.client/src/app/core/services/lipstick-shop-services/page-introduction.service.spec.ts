import { TestBed } from '@angular/core/testing';

import { PageIntroductionService } from './page-introduction.service';

describe('PageIntroductionService', () => {
  let service: PageIntroductionService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(PageIntroductionService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
