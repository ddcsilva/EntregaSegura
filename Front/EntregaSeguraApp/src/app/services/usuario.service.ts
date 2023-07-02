import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UsuarioService {
  private nome$ = new BehaviorSubject<string>("");
  private perfil$ = new BehaviorSubject<string>("");

  constructor() { }
  
  public obterPerfilDaClaim(): Observable<string> {
    return this.perfil$.asObservable();
  }

  public definirPerfilNaClaim(perfil: string) {
    this.perfil$.next(perfil);
  }

  public obterNomeDaClaim(): Observable<string> {
    return this.nome$.asObservable();
  }

  public definirNomeNaClaim(nome: string) {
    this.nome$.next(nome);
  }
}
