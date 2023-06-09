using Microsoft.AspNetCore.Identity;

namespace EntregaSegura.Infra.Data.Identity;

public class ApplicationUser : IdentityUser
{
    public int MoradorId { get; set; }
}