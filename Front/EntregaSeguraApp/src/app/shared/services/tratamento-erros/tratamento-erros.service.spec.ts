import { TestBed } from '@angular/core/testing';

import { TratamentoErrosService } from './tratamento-erros.service';

describe('TratamentoErrosService', () => {
  let service: TratamentoErrosService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(TratamentoErrosService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
