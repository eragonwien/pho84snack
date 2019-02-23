import { TestBed } from '@angular/core/testing';

import { PwaUpdateService } from './pwa-update.service';

describe('PwaUpdateService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: PwaUpdateService = TestBed.get(PwaUpdateService);
    expect(service).toBeTruthy();
  });
});
