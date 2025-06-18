import { TestBed } from '@angular/core/testing';

import { InterfaceMethodService } from './interface-method.service';

describe('InterfaceMethodService', () => {
  let service: InterfaceMethodService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(InterfaceMethodService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
