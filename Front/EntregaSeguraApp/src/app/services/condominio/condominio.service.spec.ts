import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';

import { CondominioService } from './condominio.service';
import { TratamentoErrosService } from 'src/app/shared/services/tratamento-erros/tratamento-erros.service';
import { Condominio } from 'src/app/models/condominio';

describe('CondominioService', () => {
  let service: CondominioService;
  let httpMock: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [CondominioService, TratamentoErrosService]
    });
    service = TestBed.inject(CondominioService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('Deve ser criado', () => {
    expect(service).toBeTruthy();
  });

  it('Deve criar um novo Condominio', () => {
    const condominio: Condominio = {
      id: 1,
      quantidadeUnidades: 10,
      quantidadeAndares: 5,
      quantidadeBlocos: 2,
      nome: 'Meu Condominio',
      cnpj: '12345678901234',
      telefone: '1234567890',
      email: 'condominio@example.com',
      logradouro: 'Rua Principal',
      numero: '123',
      cep: '12345-678',
      bairro: 'Centro',
      cidade: 'Cidade',
      estado: 'UF'
    };

    service.criar(condominio).subscribe((response) => {
      expect(response).toEqual(condominio);
    });

    const request = httpMock.expectOne('https://localhost:5001/api/condominios');
    expect(request.request.method).toBe('POST');
    expect(request.request.body).toEqual(condominio);
    request.flush(condominio);
  });

  it('Deve atualizar um Condominio existente', () => {
    const id = '1';
    const condominio: Condominio = {
      id: 1,
      quantidadeUnidades: 10,
      quantidadeAndares: 5,
      quantidadeBlocos: 2,
      nome: 'Meu Condominio',
      cnpj: '12345678901234',
      telefone: '1234567890',
      email: 'condominio@example.com',
      logradouro: 'Rua Principal',
      numero: '123',
      cep: '12345-678',
      bairro: 'Centro',
      cidade: 'Cidade',
      estado: 'UF'
    };

    service.atualizar(id, condominio).subscribe((response) => {
      expect(response).toEqual(condominio);
    });

    const request = httpMock.expectOne(`https://localhost:5001/api/condominios/${id}`);
    expect(request.request.method).toBe('PUT');
    expect(request.request.body).toEqual(condominio);
    request.flush(condominio);
  });

  it('Deve excluir um Condominio', () => {
    const id = '1';

    service.excluir(id).subscribe((response) => {
      expect(response).toBeNull();
    });

    const request = httpMock.expectOne(`https://localhost:5001/api/condominios/${id}`);
    expect(request.request.method).toBe('DELETE');
    request.flush(null);
  });

  it('Deve obter todos os Condominios', () => {
    const condominios: Condominio[] = [
      {
        id: 1,
        quantidadeUnidades: 10,
        quantidadeAndares: 5,
        quantidadeBlocos: 2,
        nome: 'Condominio 1',
        cnpj: '12345678901234',
        telefone: '1234567890',
        email: 'condominio1@example.com',
        logradouro: 'Rua Principal',
        numero: '123',
        cep: '12345-678',
        bairro: 'Centro',
        cidade: 'Cidade',
        estado: 'UF'
      },
      {
        id: 2,
        quantidadeUnidades: 20,
        quantidadeAndares: 7,
        quantidadeBlocos: 3,
        nome: 'Condominio 2',
        cnpj: '56789012345678',
        telefone: '9876543210',
        email: 'condominio2@example.com',
        logradouro: 'Rua Secundária',
        numero: '456',
        cep: '98765-432',
        bairro: 'Bairro',
        cidade: 'Cidade',
        estado: 'UF'
      }
    ];

    service.obterTodos().subscribe((response) => {
      expect(response).toEqual(condominios);
    });

    const request = httpMock.expectOne('https://localhost:5001/api/condominios');
    expect(request.request.method).toBe('GET');
    request.flush(condominios);
  });

  it('Deve obter um Condominio pelo ID', () => {
    const id = '1';
    const condominio: Condominio = {
      id: 1,
      quantidadeUnidades: 10,
      quantidadeAndares: 5,
      quantidadeBlocos: 2,
      nome: 'Meu Condominio',
      cnpj: '12345678901234',
      telefone: '1234567890',
      email: 'condominio@example.com',
      logradouro: 'Rua Principal',
      numero: '123',
      cep: '12345-678',
      bairro: 'Centro',
      cidade: 'Cidade',
      estado: 'UF'
    };

    service.obterPorId(id).subscribe((response) => {
      expect(response).toEqual(condominio);
    });

    const request = httpMock.expectOne(`https://localhost:5001/api/condominios/${id}`);
    expect(request.request.method).toBe('GET');
    request.flush(condominio);
  });
});