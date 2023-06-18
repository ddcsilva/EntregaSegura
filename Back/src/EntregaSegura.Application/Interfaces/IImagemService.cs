namespace EntregaSegura.Application.Interfaces;

public interface IImagemService
{
    Task<bool> SalvarImagemAsync(string imagemBase64, string nomeArquivo);
}