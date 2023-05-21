import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Condominio } from '@app/models/Condominio';
import { Observable, catchError, tap, throwError } from 'rxjs';

@Injectable()
export class CondominioService {
  urlBase: string = 'https://localhost:5001/api/condominios';

  constructor(private http: HttpClient) { }

  public obterTodos(): any {
    return this.http.get(this.urlBase);
  }

  public obterPorId(id: string): any {
    return this.http.get(`${this.urlBase}/${id}`);
  }

  public criar(condominio: Condominio): Observable<Condominio> {
    return this.http.post<Condominio>(this.urlBase, condominio);
  }

  public atualizar(id: string): Observable<Condominio> {
    return this.http.put<Condominio>(`${this.urlBase}/${id}`, id);
  }

  public excluir(id: string): Observable<any> {
    return this.http.delete(`${this.urlBase}/${id}`).pipe(
      tap({
        next: (response: any) => {
          if (response.success) {
            console.log('Condominio removido com sucesso');
          } else {
            this.handleError(response);
          }
        },
        error: (error: any) => this.handleError(error)
      })
    );
  }

  private handleError(error: HttpErrorResponse) {
    if (error.error instanceof ErrorEvent) {
      console.error('An error occurred:', error.error.message);
    } else {
      console.error(
        `Backend returned code ${error.status}, ` +
        `body was: ${error.error}`);
    }
    return throwError(() => 'Something bad happened; please try again later.');
  }

}
