using System.Text.Json;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using ecommerce.api.Classes;
using ecommerce.api.Data;
using ecommerce.api.Managers.Interfaces;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;

namespace ecommerce.api.Managers;

public class UploadManager : IUploadManager
{
    private readonly Cloudinary _cloudinary;

    public UploadManager(Cloudinary cloudinary)
    {
        _cloudinary = cloudinary;
    }
    public async Task<ImageModel> UploadImage(ImageModel image)
    {
        var file = image.File;

        if (file.Length > 0)
        {
            await using var stream = file.OpenReadStream();
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(file.Name, file.OpenReadStream()),
                Tags = "ecommerce"
            };

            var result = await _cloudinary.UploadAsync(uploadParams).ConfigureAwait(false);
            
            return new ImageModel()
            {
                Url = result.Url.ToString(),
                File = image.File,
                Id = result.PublicId,
            };
        }

        return new ImageModel()
        {
            Url = "",
            File = image.File,
            Id = ""
        };
    }
}