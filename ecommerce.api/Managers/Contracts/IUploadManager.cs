using ecommerce.api.Classes;

namespace ecommerce.api.Managers.Contracts;

public interface IUploadManager
{
    Task<ImageModel> UploadImage(IFormFile image);
}