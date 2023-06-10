using Microsoft.AspNetCore.Identity;

namespace EntregaSegura.Domain.Identity;

public class User : IdentityUser<int>
{
    public ICollection<UserRole> UserRoles { get; set; }
}