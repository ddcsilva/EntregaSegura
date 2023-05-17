/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { CondominioService } from './condominio.service';

describe('Service: Condominio', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [CondominioService]
    });
  });

  it('should ...', inject([CondominioService], (service: CondominioService) => {
    expect(service).toBeTruthy();
  }));
});
