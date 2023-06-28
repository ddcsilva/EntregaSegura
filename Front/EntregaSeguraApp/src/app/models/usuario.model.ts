import { Papel } from "./papel.model";

export interface Usuario {
    id: number;
    nome: string;
    email: string;
    foto: string;
    papeis: Papel[];
    token?: string;
}
