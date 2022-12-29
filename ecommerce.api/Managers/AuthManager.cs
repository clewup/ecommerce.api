using ecommerce.api.Classes;
using ecommerce.api.Managers.Contracts;

namespace ecommerce.api.Managers;

public class AuthManager : IAuthManager
{
    public AuthManager()
    {
    }

    public async Task<UserModel> GetUser(Guid userId)
    {
        return null;
    }
}