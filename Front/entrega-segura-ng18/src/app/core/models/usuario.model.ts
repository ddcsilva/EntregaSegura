import { Papel } from './papel.model';

export interface Usuario {
  id: number;
  nome: string;
  email: string;
  papel: Papel;
  foto?: string;
}
