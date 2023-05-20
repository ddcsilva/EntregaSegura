export interface Condominio {
    id: string;
    quantidadeUnidades: number;
    quantidadeAndares: number;
    quantidadeBlocos: number;
    nome: string;
    cnpj: string;
    telefone: string;
    email: string;
    logradouro: string;
    numero: string;
    complemento: string;
    cep: string;
    bairro: string;
    cidade: string;
    estado: string;
    dataCriacao: Date;
    dataAtualizacao: Date;
    ativo: boolean;
}