using EntregaSegura.Domain.Entities;

namespace EntregaSegura.Domain.Interfaces;

public interface IEntregaRepository : IRepositoryBase<Entrega>
{
    Task<IEnumerable<Entrega>> ObterTodasEntregasComMoradoresEUnidadesEFuncionariosETransportadorasAsync(string emailUsuario, string perfilUsuario, bool rastrearAlteracoes = false);
    Task<Entrega> ObterEntregaComMoradorEUnidadeEFuncionarioETransportadoraPorIdAsync(int id, bool rastrearAlteracoes = false);
}