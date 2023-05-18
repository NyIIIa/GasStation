import { TestBed } from '@angular/core/testing';

import { RoleAuthorisationServiceService } from './role-authorisation-service.service';

describe('RoleAuthorisationServiceService', () => {
  let service: RoleAuthorisationServiceService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(RoleAuthorisationServiceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
