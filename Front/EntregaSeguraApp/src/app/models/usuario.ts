import { Role } from "./role";

export interface Usuario {
    id: number;
    nome: string;
    email: string;
    foto: string;
    roles: Role[];
    token?: string;
}
