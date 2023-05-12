namespace EntregaSegura.Domain.Validators.Helpers;

public static class CNPJValidation
{
    public static bool ValidarCNPJ(string cnpj)
    {
        if (string.IsNullOrWhiteSpace(cnpj))
            return false;

        cnpj = cnpj.Trim().Replace(".", "").Replace("-", "").Replace("/", "");

        if (cnpj.Length != 14)
            return false;

        if (cnpj == "00000000000000" ||
            cnpj == "11111111111111" ||
            cnpj == "22222222222222" ||
            cnpj == "33333333333333" ||
            cnpj == "44444444444444" ||
            cnpj == "55555555555555" ||
            cnpj == "66666666666666" ||
            cnpj == "77777777777777" ||
            cnpj == "88888888888888" ||
            cnpj == "99999999999999")
            return false;

        int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
        char[] digito = new char[2];
        int[] soma = new int[2] { 0, 0 };

        for (int i = 0; i < 12; i++)
            soma[0] += int.Parse(cnpj[i].ToString()) * multiplicador1[i];

        soma[0] %= 11;

        if (soma[0] < 2)
            soma[0] = 0;
        else
            soma[0] = 11 - soma[0];

        for (int i = 0; i < 13; i++)
            soma[1] += int.Parse(cnpj[i].ToString()) * multiplicador1[i];

        soma[1] %= 11;

        if (soma[1] < 2)
            soma[1] = 0;
        else
            soma[1] = 11 - soma[1];

        return cnpj.EndsWith(soma[0].ToString() + soma[1].ToString());
    }
}