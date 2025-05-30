import { Usuario } from './usuario.model';

export interface EstadoAutenticacao {
  usuario: Usuario | null;
  token: string | null;
  carregando: boolean;
  erro: string | null;
}
