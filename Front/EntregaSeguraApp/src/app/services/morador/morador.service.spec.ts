/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { MoradorService } from './morador.service';

describe('Service: Morador', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [MoradorService]
    });
  });

  it('should ...', inject([MoradorService], (service: MoradorService) => {
    expect(service).toBeTruthy();
  }));
});
