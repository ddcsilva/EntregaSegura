import { PerfilUsuario } from "./enums/perfil-usuario.enum";
import { Pessoa } from "./pessoa.model";

export interface Usuario {
    id: number;
    nome: string;
    login: string;
    senha: string;
    token: string;
    perfilUsuario: PerfilUsuario;
    pessoa: Pessoa
}