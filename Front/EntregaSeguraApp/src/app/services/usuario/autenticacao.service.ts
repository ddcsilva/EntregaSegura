import { Injectable } from "@angular/core";
import { Router } from "@angular/router";
import { HttpClient } from "@angular/common/http";
import { BehaviorSubject, Observable, map } from "rxjs";

import { Usuario } from "@app/models/usuario";
import { environment } from "@environments/environment";

@Injectable()
export class AutenticacaoService {
  private usuarioSubject: BehaviorSubject<Usuario | null>;
  public usuario: Observable<Usuario | null>;

  constructor(private router: Router, private http: HttpClient) {
    this.usuarioSubject = new BehaviorSubject(JSON.parse(localStorage.getItem('usuario')!));
    this.usuario = this.usuarioSubject.asObservable();
  }

  public get usuarioAutenticado() {
    return this.usuarioSubject.value;
  }

  login(email: string, senha: string) {
    return this.http.post<any>(`${environment.apiUrl}/conta/login`, { email, senha })
      .pipe(map(response => {
        const usuario = response.data;
        // Armazena os detalhes do usuário e o token jwt no armazenamento local para manter o usuário conectado entre as atualizações da página
        localStorage.setItem('usuario', JSON.stringify(usuario));
        this.usuarioSubject.next(usuario);
        return usuario;
      }));
  }

  logout() {
    // Remove o usuário do armazenamento local e define o usuário atual como nulo
    localStorage.removeItem('usuario');
    this.usuarioSubject.next(null);
    this.router.navigate(['/login']);
  }
}
