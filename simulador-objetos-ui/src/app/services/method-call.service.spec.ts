import { TestBed } from '@angular/core/testing';

import { MethodCallService } from './method-call.service';

describe('MethodCallService', () => {
  let service: MethodCallService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(MethodCallService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
