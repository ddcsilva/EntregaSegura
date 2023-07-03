import { Pessoa } from "./pessoa.model";

export interface Funcionario {
    id: number;
    cargo: string;
    dataAdmissao: Date;
    dataDemissao?: Date;
    userId: number;
    nomeCondominio: string;
    pessoa: Pessoa;
}