using System.Security.Claims;
using ecommerce.api.Classes;
using ecommerce.api.Infrastructure;

namespace ecommerce.api.Managers;

public class ClaimsManager
{
    public UserModel GetUser(HttpRequest request)
    {
        var claimsIdentity = GetClaimsIdentity(request.HttpContext);

        var idClaim = claimsIdentity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
        var emailClaim = claimsIdentity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email);
        var roleClaim = claimsIdentity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);

        if (idClaim != null && emailClaim != null)
        {
            return new UserModel()
            {
                Id = Guid.Parse(idClaim.Value),
                Email = emailClaim.Value,
                Role = roleClaim?.Value ?? RoleType.User
            };
        }
        else
        {
            return null;
        }
    }

    public ClaimsIdentity GetClaimsIdentity(HttpContext context)
    {
        if (context.User != null && 
            context.User.Identity != null && 
            context.User.Identity.IsAuthenticated)
        {
            return context.User.Identity as ClaimsIdentity;;
        }
        else
        {
            throw new Exception("No claims found.");
        }
    }
}