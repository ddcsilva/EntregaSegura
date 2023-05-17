import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable()
export class CondominioService {
  urlBase: string = 'https://localhost:5001/api/condominios';

  constructor(private http: HttpClient) { }

  public getCondominios(): any {
    return this.http.get(this.urlBase);
  }

}
