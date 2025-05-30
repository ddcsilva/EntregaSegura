import { Usuario } from './usuario.model';

export interface AuthState {
  user: Usuario | null;
  token: string | null;
  isLoading: boolean;
  error: string | null;
}
