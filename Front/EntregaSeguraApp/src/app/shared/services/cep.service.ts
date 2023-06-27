// Angular imports
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

// Library imports
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CepService {
  private urlBase: string = 'https://viacep.com.br/ws';

  constructor(private http: HttpClient) { }

  public buscarPorCep(cep: string): Observable<any> {
    const url = `${this.urlBase}/${cep}/json`;
    return this.http.get(url);
  }
}