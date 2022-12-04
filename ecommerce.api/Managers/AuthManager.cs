using ecommerce.api.Classes;
using ecommerce.api.Data;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace ecommerce.api.Managers;

public class AuthManager
{
    public AuthManager()
    {
    }

    // TODO: Implement call to the Auth API.
    public async Task<UserModel> GetUser(Guid userId)
    {
        return null;
    }
}