import { Pessoa } from "./pessoa.model";

export interface Morador {
    id: number;
    ramal: string;
    userId: number;
    condominioId: number;
    unidadeId: number;
    nomeCondominio: string;
    descricaoUnidade: string;
    pessoa: Pessoa;
}