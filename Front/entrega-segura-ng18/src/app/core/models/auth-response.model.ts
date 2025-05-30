import { Usuario } from './usuario.model';

export interface AuthResponse {
  token: string;
  user: Usuario;
  expires?: string;
}
