import { TestBed } from '@angular/core/testing';

import { StudyProgressService } from './study-progress.service';

describe('StudyProgressService', () => {
  let service: StudyProgressService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(StudyProgressService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
