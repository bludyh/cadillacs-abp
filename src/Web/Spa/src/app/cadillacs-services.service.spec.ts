import { TestBed } from '@angular/core/testing';

import { CadillacsServicesService } from './cadillacs-services.service';

describe('CadillacsServicesService', () => {
  let service: CadillacsServicesService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CadillacsServicesService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
