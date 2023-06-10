using EntregaSegura.Application.DTOs;

namespace EntregaSegura.Application.Interfaces;

public interface IMoradorService
{
    Task<IEnumerable<MoradorDTO>> ObterTodosMoradoresAsync();
    Task<MoradorDTO> ObterMoradorPorIdAsync(int id);

    Task<bool> AdicionarAsync(MoradorDTO morador);
    Task<bool> AtualizarAsync(MoradorDTO morador);
    Task<bool> RemoverAsync(int id);
}