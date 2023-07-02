using System.Security.Cryptography;

namespace EntregaSegura.Application.Helpers;

public class Criptografia
{
    private static readonly int SaltSize = 16;
    private static readonly int HashSize = 20;
    private static readonly int Iterations = 10000;

    public static string CriptografarSenha(string senha)
    {
        byte[] salt = new byte[SaltSize];
        RandomNumberGenerator.Fill(salt);

        var key = new Rfc2898DeriveBytes(senha, salt, Iterations);
        var hash = key.GetBytes(HashSize);
        var hashBytes = new byte[SaltSize + HashSize];

        Array.Copy(salt, 0, hashBytes, 0, SaltSize);
        Array.Copy(hash, 0, hashBytes, SaltSize, HashSize);

        var base64Hash = Convert.ToBase64String(hashBytes);
        return base64Hash;
    }

    public static bool VerificarSenha(string senha, string hashSenha)
    {
        var hashBytes = Convert.FromBase64String(hashSenha);
        var salt = new byte[SaltSize];

        Array.Copy(hashBytes, 0, salt, 0, SaltSize);

        var key = new Rfc2898DeriveBytes(senha, salt, Iterations);
        var hash = key.GetBytes(HashSize);

        for (var i = 0; i < HashSize; i++)
        {
            if (hashBytes[i + SaltSize] != hash[i])
                return false;
        }
        return true;
    }
}