namespace EntregaSegura.Domain.Validators.Helpers;

public static class EstadoValidation
{
    private static readonly string[] EstadosBrasileiros = new[]
    {
        "AC", "AL", "AP", "AM", "BA", "CE", "DF", "ES", "GO",
        "MA", "MT", "MS", "MG", "PA", "PB", "PR", "PE", "PI",
        "RJ", "RN", "RS", "RO", "RR", "SC", "SP", "SE", "TO"
    };

    public static bool ValidarEstado(string estado)
    {
        return EstadosBrasileiros.Contains(estado);
    }
}
