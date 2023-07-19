using EntregaSegura.Domain.Entities.Enums;

namespace EntregaSegura.Domain.Entities;

public sealed class Entrega : EntityBase
{
    public Entrega(string descricao, int transportadoraId, int moradorId, int funcionarioId)
    {
        Descricao = descricao;
        TransportadoraId = transportadoraId;
        MoradorId = moradorId;
        FuncionarioId = funcionarioId;
        Status = StatusEntrega.Recebida;
    }

    public string Descricao { get; private set; }
    public DateTime DataRecebimento { get; private set; }
    public DateTime? DataRetirada { get; private set; }
    public StatusEntrega Status { get; private set; }
    public int TransportadoraId { get; private set; }
    public int MoradorId { get; private set; }
    public int FuncionarioId { get; private set; }

    // Propriedades de Navegação
    public Transportadora Transportadora { get; private set; }
    public Morador Morador { get; private set; }
    public Funcionario Funcionario { get; private set; }

    public void AtualizarParaNotificada()
    {
        Status = StatusEntrega.Notificada;
    }

    public void AtualizarParaRetirada()
    {
        DataRetirada = DateTime.Now;
        Status = StatusEntrega.Retirada;
    }
}