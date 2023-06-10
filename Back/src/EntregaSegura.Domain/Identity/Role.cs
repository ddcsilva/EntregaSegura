using Microsoft.AspNetCore.Identity;

namespace EntregaSegura.Domain.Identity;

public class Role : IdentityRole<int>
{
    public ICollection<UserRole> UserRoles { get; set; }
}