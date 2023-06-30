using EntregaSegura.Application.DTOs;
using EntregaSegura.Domain.Entities;

namespace EntregaSegura.Application.Interfaces;

public interface IEntregaService : IDisposable
{
    Task<IEnumerable<EntregaDTO>> ObterTodasEntregasAsync();
    Task<EntregaDTO> ObterEntregaPorIdAsync(int id, bool rastrearAlteracoes = false);

    Task<bool> AdicionarAsync(EntregaDTO entrega);
    Task<bool> AtualizarAsync(EntregaDTO entrega);
    Task<bool> RemoverAsync(int id);

    Task<IEnumerable<EntregaDTO>> ObterTodasEntregasComMoradoresEUnidadesEFuncionariosETransportadorasAsync();

    Task<IEnumerable<Entrega>> ObterEntregasPorMoradorAsync(int moradorId);
    Task<IEnumerable<Entrega>> ObterEntregasPorFuncionarioAsync(int funcionarioId);
    Task<IEnumerable<Entrega>> ObterEntregasPorTransportadoraAsync(int transportadoraId);
    Task<IEnumerable<Entrega>> ObterEntregasComStatusAguardandoRetiradaPorMoradorAsync(int moradorId);
    Task<IEnumerable<Entrega>> ObterEntregasComStatusRetiradaPorMoradorAsync(int moradorId);
    Task<IEnumerable<Entrega>> ObterEntregasRecebidasPorFuncionarioAsync(int funcionarioId);
}