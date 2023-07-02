import { PerfilUsuario } from "./enums/perfil-usuario.model";

export interface Usuario {
    id: number;
    nome: string;
    login: string;
    senha: string;
    email: string;
    token: string;
    perfilUsuario: PerfilUsuario;
}