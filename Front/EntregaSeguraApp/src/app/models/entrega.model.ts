export interface Entrega {
    id: number;
    descricao: string;
    dataRecebimento: Date;
    DataRetirada?: Date;
    status: string;
    transportadoraId: number;
    nomeTransportadora: string;
    moradorId: number;
    nomeMorador: string;
    funcionarioId: number;
    nomeFuncionario: string;
    descricaoUnidade: string;
}