using EntregaSegura.Application.Interfaces;

namespace EntregaSegura.Application.Services;

public class ImagemService : IImagemService
{
    public async Task<bool> SalvarImagemAsync(string imagemBase64, string nomeArquivo)
    {
        if (string.IsNullOrEmpty(imagemBase64))
        {
            return false;
        }

        var imagemBytes = Convert.FromBase64String(imagemBase64);

        var caminhoArquivo = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", nomeArquivo);

        await File.WriteAllBytesAsync(caminhoArquivo, imagemBytes);

        return true;
    }
}