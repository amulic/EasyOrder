import { TestBed } from '@angular/core/testing';

import { LocalStoreService } from './local-store.service';

describe('UserStoreService', () => {
  let service: LocalStoreService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(LocalStoreService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
