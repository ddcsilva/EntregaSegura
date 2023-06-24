// import { HttpClient } from '@angular/common/http';
// import { Injectable } from '@angular/core';
// import { map } from 'rxjs';
// import { Usuario } from 'src/app/models/usuario';

// @Injectable()
// export class ContaService {
//   private urlBase: string = 'https://localhost:5001/api/conta';

//   constructor(private http: HttpClient) { }

//   public login(email: string, senha: string) {
//     return this.http.post<any>(`${this.urlBase}/login`, { email, senha })
//       .pipe(map(resposta => {
//         if (resposta && resposta.success && resposta.data.token) {
//           let usuario: Usuario = {
//             email: resposta.data.email,
//             token: resposta.data.token
//           };
//           localStorage.setItem('usuarioAtual', JSON.stringify(usuario));
//         }
//         return resposta;
//       }));
//   }

//   public get autenticado(): boolean {
//     return (localStorage.getItem('usuarioAtual') !== null);
//   }

//   public logout() {
//     localStorage.removeItem('usuarioAtual');
//   }
// }
