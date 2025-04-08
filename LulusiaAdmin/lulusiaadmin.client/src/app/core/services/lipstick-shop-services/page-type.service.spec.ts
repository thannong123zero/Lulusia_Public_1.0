import { TestBed } from '@angular/core/testing';

import { PageTypeService } from './page-type.service';

describe('PageTypeService', () => {
  let service: PageTypeService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(PageTypeService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
