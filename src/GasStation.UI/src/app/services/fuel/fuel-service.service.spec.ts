import { TestBed } from '@angular/core/testing';

import { FuelServiceService } from './fuel-service.service';

describe('FuelServiceService', () => {
  let service: FuelServiceService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(FuelServiceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
