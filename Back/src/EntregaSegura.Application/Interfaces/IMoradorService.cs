using EntregaSegura.Application.DTOs;

namespace EntregaSegura.Application.Interfaces;

public interface IMoradorService
{
    Task<IEnumerable<MoradorDTO>> ObterTodosMoradoresAsync();
    Task<MoradorDTO> ObterMoradorPorIdAsync(int id);

    Task AdicionarAsync(MoradorDTO morador);
    Task AtualizarAsync(MoradorDTO morador);
    Task RemoverAsync(int id);
}