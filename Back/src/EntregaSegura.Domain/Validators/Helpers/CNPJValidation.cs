namespace EntregaSegura.Domain.Validators.Helpers;

public static class CNPJValidation
{
    public static bool ValidarCNPJ(string cnpj)
    {
        if (VerificarSeEstaVazioOuNulo(cnpj))
            return false;

        if (!VerificarTamanho(cnpj) || VerificarSequenciaInvalida(cnpj))
            return false;

        string primeiraParte = cnpj.Substring(0, 12);
        string digitoCalculado = CalcularDigito(primeiraParte);

        return cnpj.EndsWith(digitoCalculado);
    }

    private static bool VerificarSeEstaVazioOuNulo(string cnpj)
    {
        return string.IsNullOrWhiteSpace(cnpj);
    }

    private static bool VerificarTamanho(string cnpj)
    {
        return cnpj.Length == 14;
    }

    private static bool VerificarSequenciaInvalida(string cnpj)
    {
        return cnpj == "00000000000000" ||
            cnpj == "11111111111111" ||
            cnpj == "22222222222222" ||
            cnpj == "33333333333333" ||
            cnpj == "44444444444444" ||
            cnpj == "55555555555555" ||
            cnpj == "66666666666666" ||
            cnpj == "77777777777777" ||
            cnpj == "88888888888888" ||
            cnpj == "99999999999999";
    }

    private static string CalcularDigito(string cnpj)
    {
        int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
        int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
        int soma;
        string digito;
        string tempCnpj;

        soma = 0;
        tempCnpj = cnpj;

        for (int i = 0; i < 12; i++)
            soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];

        digito = (soma % 11 < 2) ? "0" : (11 - soma % 11).ToString();
        tempCnpj = tempCnpj + digito;
        soma = 0;

        for (int i = 0; i < 13; i++)
            soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];

        digito = digito + ((soma % 11 < 2) ? "0" : (11 - soma % 11).ToString());

        return digito;
    }
}