import { TestBed } from '@angular/core/testing';

import { UnidadeService } from './unidade.service';

describe('UnidadeService', () => {
  let service: UnidadeService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(UnidadeService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
