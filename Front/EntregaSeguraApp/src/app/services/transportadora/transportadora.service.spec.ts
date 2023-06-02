import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';

import { TransportadoraService } from './transportadora.service';
import { TratamentoErrosService } from 'src/app/shared/services/tratamento-erros/tratamento-erros.service';
import { Transportadora } from 'src/app/models/transportadora';

describe('TransportadoraService', () => {
  let service: TransportadoraService;
  let httpMock: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [TransportadoraService, TratamentoErrosService]
    });
    service = TestBed.inject(TransportadoraService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('Deve ser criado', () => {
    expect(service).toBeTruthy();
  });

  it('Deve criar uma nova Transportadora', () => {
    const transportadora: Transportadora = {
      id: 1,
      nome: 'Minha Transportadora',
      cnpj: '12345678901234',
      email: 'transportadora@example.com',
      telefone: '1234567890'
    };

    service.criar(transportadora).subscribe((response) => {
      expect(response).toEqual(transportadora);
    });

    const request = httpMock.expectOne('https://localhost:5001/api/transportadoras');
    expect(request.request.method).toBe('POST');
    expect(request.request.body).toEqual(transportadora);
    request.flush(transportadora);
  });

  it('Deve atualizar uma Transportadora existente', () => {
    const id = '1';
    const transportadora: Transportadora = {
      id: 1,
      nome: 'Minha Transportadora Atualizada',
      cnpj: '12345678901234',
      email: 'transportadora@example.com',
      telefone: '1234567890'
    };

    service.atualizar(id, transportadora).subscribe((response) => {
      expect(response).toEqual(transportadora);
    });

    const request = httpMock.expectOne(`https://localhost:5001/api/transportadoras/${id}`);
    expect(request.request.method).toBe('PUT');
    expect(request.request.body).toEqual(transportadora);
    request.flush(transportadora);
  });

  it('Deve excluir uma Transportadora', () => {
    const id = '1';

    service.excluir(id).subscribe((response) => {
      expect(response).toBeNull();
    });

    const request = httpMock.expectOne(`https://localhost:5001/api/transportadoras/${id}`);
    expect(request.request.method).toBe('DELETE');
    request.flush(null);
  });

  it('Deve obter todas as Transportadoras', () => {
    const transportadoras: Transportadora[] = [
      {
        id: 1,
        nome: 'Transportadora 1',
        cnpj: '12345678901234',
        email: 'transportadora1@example.com',
        telefone: '1234567890'
      },
      {
        id: 2,
        nome: 'Transportadora 2',
        cnpj: '56789012345678',
        email: 'transportadora2@example.com',
        telefone: '9876543210'
      }
    ];

    service.obterTodos().subscribe((response) => {
      expect(response).toEqual(transportadoras);
    });

    const request = httpMock.expectOne('https://localhost:5001/api/transportadoras');
    expect(request.request.method).toBe('GET');
    request.flush(transportadoras);
  });

  it('Deve obter uma Transportadora pelo ID', () => {
    const id = '1';
    const transportadora: Transportadora = {
      id: 1,
      nome: 'Minha Transportadora',
      cnpj: '12345678901234',
      email: 'transportadora@example.com',
      telefone: '1234567890'
    };

    service.obterPorId(id).subscribe((response) => {
      expect(response).toEqual(transportadora);
    });

    const request = httpMock.expectOne(`https://localhost:5001/api/transportadoras/${id}`);
    expect(request.request.method).toBe('GET');
    request.flush(transportadora);
  });
});
