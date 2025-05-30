import { Papel } from './papel.model';

export interface Usuario {
  id: number;
  nome: string;
  email: string;
  perfil: Papel;
  foto?: string;
}
