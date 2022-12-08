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

    public async Task<ImageModel> UploadImage(ImageModel image)
    {
        var file = image.File;

        using (var stream = new MemoryStream())
        {
            await file.CopyToAsync(stream);
            stream.Position = 0;

            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(file.Name, stream),
                PublicId = "products",
            };

            var uploadResult = await _cloudinary.UploadAsync(uploadParams);

            return new ImageModel()
            {
                Id = uploadResult.PublicId,
                Url = uploadResult.Url,
                File = image.File,
            };
        }
    }
}