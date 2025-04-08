import { TestBed } from '@angular/core/testing';

import { HomeBannerService } from './home-banner.service';

describe('HomeBannerService', () => {
  let service: HomeBannerService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(HomeBannerService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
