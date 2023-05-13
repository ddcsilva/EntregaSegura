namespace EntregaSegura.Domain.Validators.Helpers;
using System.Text.RegularExpressions;

public static class TelefoneValidation
{
    private static readonly Regex _regexTelefone = new Regex(@"^([1-9]{2})?(9?[2-9]{1}[0-9]{3}[0-9]{4})$");

    public static bool ValidarTelefone(string telefone)
    {
        if (string.IsNullOrWhiteSpace(telefone))
            return false;
            
        telefone = RemoverCaracteresNaoNumericos(telefone);

        return NumeroTelefoneEhValido(telefone);
    }

    private static string RemoverCaracteresNaoNumericos(string telefone)
    {
        return Regex.Replace(telefone, @"[^\d]", "");
    }

    private static bool NumeroTelefoneEhValido(string telefone)
    {
        return _regexTelefone.IsMatch(telefone);
    }
}
