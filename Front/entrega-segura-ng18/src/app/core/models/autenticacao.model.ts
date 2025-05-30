import { Usuario } from './usuario.model';

export interface Autenticacao {
  token: string;
  usuario: Usuario;
  expiracao?: string;
}
