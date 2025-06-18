import { TestBed } from '@angular/core/testing';

import { LocalVarService } from './local-var.service';

describe('LocalVarService', () => {
  let service: LocalVarService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(LocalVarService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
