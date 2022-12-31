using System.Security.Claims;
using ecommerce.api.Models;

namespace ecommerce.api.Managers.Contracts;

public interface IClaimsManager
{
    UserModel GetUser(HttpRequest request);
    ClaimsIdentity GetClaimsIdentity(HttpContext context);
}