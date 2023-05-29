import { TestBed } from '@angular/core/testing';

import { TransportadoraService } from './transportadora.service';

describe('TransportadoraService', () => {
  let service: TransportadoraService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(TransportadoraService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
