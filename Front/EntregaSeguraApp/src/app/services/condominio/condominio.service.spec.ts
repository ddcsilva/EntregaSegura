import { TestBed } from '@angular/core/testing';

import { CondominioService } from './condominio.service';

describe('CondominioService', () => {
  let service: CondominioService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CondominioService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
