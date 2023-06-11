using System.Security.Claims;

namespace EntregaSegura.API.Extensions;

public static class ClaimsExtension
{
    public static string ObterUserName(this ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal.FindFirstValue(ClaimTypes.Name);
    }

    public static int ObterUserId(this ClaimsPrincipal claimsPrincipal)
    {
        return int.Parse(claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier));
    }
}