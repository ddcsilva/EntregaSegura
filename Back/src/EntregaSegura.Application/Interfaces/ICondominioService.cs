using EntregaSegura.Domain.Entities;

namespace EntregaSegura.Application.Interfaces;

public interface ICondominioService
{
    Task Adicionar(Condominio condominio);
    Task Atualizar(Condominio condominio);
    Task Remover(Guid id);
}