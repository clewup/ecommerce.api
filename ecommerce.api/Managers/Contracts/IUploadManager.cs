namespace ecommerce.api.Managers.Contracts;

public interface IUploadManager
{
    Task<string> UploadImage(IFormFile image);
}