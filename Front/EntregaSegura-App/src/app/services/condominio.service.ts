import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Condominio } from '@app/models/Condominio';
import { Observable } from 'rxjs';

@Injectable()
export class CondominioService {
  urlBase: string = 'https://localhost:5001/api/condominios';

  constructor(private http: HttpClient) { }

  public getCondominios(): any {
    return this.http.get(this.urlBase);
  }

  public getCondominio(id: string): any {
    return this.http.get(`${this.urlBase}/${id}`);
  }

  public postCondominio(condominio: Condominio): Observable<Condominio> {
    return this.http.post<Condominio>(this.urlBase, condominio);
  }

  public putCondominio(id: string): Observable<Condominio> {
    return this.http.put<Condominio>(`${this.urlBase}/${id}`, id);
  }

  public deleteCondominio(id: string): Observable<string> {
    return this.http.delete<string>(`${this.urlBase}/${id}`);
  }
}
