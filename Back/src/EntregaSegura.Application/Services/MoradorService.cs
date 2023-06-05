// using EntregaSegura.Application.Interfaces;
// using EntregaSegura.Domain.Entities;
// using EntregaSegura.Domain.Interfaces.Repositories;
// using EntregaSegura.Domain.Validators;
// using EntregaSegura.Infrastructure.UnitOfWork;

// namespace EntregaSegura.Application.Services;

// public class MoradorService : BaseService, IMoradorService
// {
//     private readonly IMoradorRepository _moradorRepository;

//     public MoradorService(IMoradorRepository moradorRepository,
//                           IUnitOfWork unitOfWork,
//                           INotificadorErros notificadorErros) : base(unitOfWork, notificadorErros)
//     {
//         _moradorRepository = moradorRepository;
//     }

//     public async Task<Morador> Adicionar(Morador morador)
//     {
//         if (!ExecutarValidacao(new MoradorValidator(), morador)) return null;

//         if (_moradorRepository.BuscarAsync(m => m.CPF == morador.CPF).Result.Any())
//         {
//             Notificar("Já existe um morador com este CPF.");
//             return null;
//         }

//         _moradorRepository.Adicionar(morador);
//         await CommitAsync();

//         return morador;
//     }

//     public async Task<Morador> Atualizar(Morador morador)
//     {
//         if (!ExecutarValidacao(new MoradorValidator(), morador)) return null;

//         if (_moradorRepository.BuscarAsync(m => m.CPF == morador.CPF && m.Id != morador.Id).Result.Any())
//         {
//             Notificar("Já existe um morador com este CPF.");
//             return null;
//         }

//         _moradorRepository.Atualizar(morador);
//         await CommitAsync();

//         return morador;
//     }

//     public async Task<bool> Remover(int id)
//     {
//         var morador = await _moradorRepository.ObterPorIdAsync(id);

//         if (morador == null)
//         {
//             Notificar("Morador não encontrado.");
//             return false;
//         }

//         _moradorRepository.Remover(morador);
//         await CommitAsync();

//         return true;
//     }

//     public async Task<IEnumerable<Morador>> ObterTodosAsync()
//     {
//         return await _moradorRepository.ObterTodosAsync();
//     }

//     public async Task<Morador> ObterPorIdAsync(int id)
//     {
//         return await _moradorRepository.ObterPorIdAsync(id);
//     }

//     public async Task<Morador> ObterPorNomeAsync(string nome)
//     {
//         return await _moradorRepository.ObterPorNomeAsync(nome);
//     }

//     public async Task<Morador> ObterMoradorComEntregasAsync(int moradorId)
//     {
//         return await _moradorRepository.ObterMoradorComEntregasAsync(moradorId);
//     }

//     public async Task<IEnumerable<Morador>> ObterMoradoresPorUnidadeAsync(int unidadeId)
//     {
//         return await _moradorRepository.ObterMoradoresPorUnidadeAsync(unidadeId);
//     }

//     public async Task<Morador> ObterMoradorComUnidadeAsync(int moradorId)
//     {
//         return await _moradorRepository.ObterMoradorComUnidadeAsync(moradorId);
//     }

//     public void Dispose()
//     {
//         _unitOfWork?.Dispose();
//     }
// }