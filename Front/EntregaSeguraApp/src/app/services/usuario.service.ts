import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { TratamentoErrosService } from '@app/shared/services/tratamento-erros.service';
import { environment } from '@environments/environment';
import { BehaviorSubject, Observable, catchError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UsuarioService {
  private urlBaseApi = `${environment.urlBaseApi}/api`;
  private nome$ = new BehaviorSubject<string>("");
  private email$ = new BehaviorSubject<string>("");
  private perfil$ = new BehaviorSubject<string>("");
  private foto$ = new BehaviorSubject<string>("");

  constructor(private httpClient: HttpClient) { }

  public obterPerfilDaClaim(): Observable<string> {
    return this.perfil$.asObservable();
  }

  public definirPerfilNaClaim(perfil: string) {
    this.perfil$.next(perfil);
  }

  public obterNomeDaClaim(): Observable<string> {
    return this.nome$.asObservable();
  }

  public obterEmailDaClaim(): Observable<string> {
    return this.email$.asObservable();
  }

  public definirEmailNaClaim(email: string) {
    this.email$.next(email);
  }

  public definirNomeNaClaim(nome: string) {
    this.nome$.next(nome);
  }

  public obterFotoDaClaim(): Observable<string> {
    return this.foto$.asObservable();
  }

  public definirFotoNaClaim(foto: string) {
    this.foto$.next(foto);
  }

  public uploadFoto(login: string, foto: File): Observable<any> {
    const url = `${this.urlBaseApi}/usuario/carregar-foto/${login}`;
    const formData = new FormData();
    formData.append('foto', foto);

    return this.httpClient.post(url, formData, { responseType: 'text' });
  }

  public obterCaminhoFoto(caminhoRelativo: string): string {
    return `${environment.urlBaseApi}/${caminhoRelativo}`;
  }
}