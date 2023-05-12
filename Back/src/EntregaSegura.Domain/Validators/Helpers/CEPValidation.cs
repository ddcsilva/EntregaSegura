using Polly;
using Polly.Timeout;

namespace EntregaSegura.Domain.Validators.Helpers;

public static class CEPValidation
{
    private static readonly AsyncTimeoutPolicy _timeoutPolicy = Policy.TimeoutAsync(2); // 2 segundos

    public static bool ValidarFormatoCEP(string cep)
    {
        if (string.IsNullOrWhiteSpace(cep))
            return false;

        cep = cep.Trim().Replace("-", "");

        if (cep.Length != 8)
            return false;

        return true;
    }

    public static async Task<bool> ValidarCEP(string cep)
    {
        if (!ValidarFormatoCEP(cep))
            return false;

        try
        {
            await _timeoutPolicy.ExecuteAsync(async token =>
            {
                using (var client = new HttpClient())
                {
                    var response = await client.GetAsync($"https://viacep.com.br/ws/{cep}/json/", token);
                    if (!response.IsSuccessStatusCode)
                    {
                        throw new Exception("CEP não encontrado");
                    }
                }
            }, CancellationToken.None);
        }
        catch (TimeoutRejectedException)
        {
            return true;
        }
        catch (Exception)
        {
            return false;
        }

        return true;
    }
}
