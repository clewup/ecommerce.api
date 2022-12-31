using ecommerce.api.Classes;
using ecommerce.api.Entities;

namespace ecommerce.api.DataManagers.Contracts;

public interface IImageDataManager
{
    Task<List<ImageEntity>> GetImages(Guid productId);
    Task UploadImage(string imageUrl, ProductEntity product, UserModel user);
}