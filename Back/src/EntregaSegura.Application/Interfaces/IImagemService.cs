using Microsoft.AspNetCore.Http;

namespace EntregaSegura.Application.Interfaces;

public interface IImagemService
{
    Task<string> Carregar(IFormFile arquivo, string nomeArquivo);
}