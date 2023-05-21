using EntregaSegura.Domain.Entities.Enums;

namespace EntregaSegura.Domain.Entities;

public sealed class Entrega : BaseEntity
{
    public Entrega()
    {
        DataRecebimento = DateTime.Now;
        Status = StatusEntrega.Recebida;        
    }

    public int TransportadoraId { get; set; }
    public int MoradorId { get; set; }
    public int FuncionarioId { get; set; }
    public DateTime DataRecebimento { get; set; }
    public DateTime? DataRetirada { get; set; }
    public string Descricao { get; set; }
    public string Observacao { get; set; }
    public StatusEntrega Status { get; set; }

    // Uma entrega pertence a apenas uma transportadora
    public Transportadora Transportadora { get; set; }

    // Uma entrega pertence a apenas um morador
    public Morador Morador { get; set; }

    // Uma entrega só pode ser manipulada por um funcionário
    public Funcionario Funcionario { get; set; }
}