using EntregaSegura.Application.Interfaces;
using Microsoft.AspNetCore.Http;

namespace EntregaSegura.Application.Services;

public class ImagemService : IImagemService
{
    public async Task<string> Carregar(IFormFile arquivo, string nomeArquivo)
    {
        var caminhoArquivo = Path.Combine(Directory.GetCurrentDirectory(), @"Resources\Imagens", nomeArquivo);
        using Stream stream = new FileStream(caminhoArquivo, FileMode.Create);
        await arquivo.CopyToAsync(stream);

        return ObterCaminhoRelativoServidor(nomeArquivo);
    }

    private string ObterCaminhoRelativoServidor(string nomeArquivo)
    {
        return Path.Combine(@"Resources\Imagens", nomeArquivo);
    }
}