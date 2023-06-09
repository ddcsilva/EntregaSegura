using EntregaSegura.Domain.Entities.Enums;

namespace EntregaSegura.Domain.Entities;

public sealed class Entrega : BaseEntity
{
    public Entrega(string descricao, string observacao, int transportadoraId, int moradorId, int funcionarioId)
    {
        Descricao = descricao;
        Observacao = observacao;
        TransportadoraId = transportadoraId;
        MoradorId = moradorId;
        FuncionarioId = funcionarioId;
        Status = StatusEntrega.Recebida;
    }

    public DateTime DataRecebimento { get; private set; }
    public DateTime? DataRetirada { get; private set; }
    public string Descricao { get; private set; }
    public string Observacao { get; private set; }
    public StatusEntrega Status { get; private set; }

    public int TransportadoraId { get; private set; }
    public Transportadora Transportadora { get; private set; }
    public int MoradorId { get; private set; }
    public Morador Morador { get; private set; }
    public int FuncionarioId { get; private set; }
    public Funcionario Funcionario { get; private set; }
}