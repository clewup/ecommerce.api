using ecommerce.api.Classes;

namespace ecommerce.api.Managers.Contracts;

public interface IAuthManager
{
    Task<UserModel> GetUser(Guid userId);
}