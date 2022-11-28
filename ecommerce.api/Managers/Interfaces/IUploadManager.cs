using ecommerce.api.Classes;

namespace ecommerce.api.Managers.Interfaces;

public interface IUploadManager
{
    Task<ImageModel> UploadImage(ImageModel image);
}