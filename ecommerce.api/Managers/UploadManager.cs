using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using ecommerce.api.Classes;

namespace ecommerce.api.Managers;

public class UploadManager
{
    private readonly Cloudinary _cloudinary;

    public UploadManager(Cloudinary cloudinary)
    {
        _cloudinary = cloudinary;
    }

    public async Task<ImageModel> UploadImage(IFormFile image)
    {

        using (var stream = new MemoryStream())
        {
            await image.CopyToAsync(stream);
            stream.Position = 0;

            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(image.Name, stream),
                PublicId = "ecommerce/products",
            };

            var uploadResult = await _cloudinary.UploadAsync(uploadParams);

            return new ImageModel()
            {
                Id = uploadResult.PublicId,
                Url = uploadResult.Url,
            };
        }
    }
}