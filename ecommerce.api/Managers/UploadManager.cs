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
    static readonly HttpClient _client = new HttpClient();
    private readonly IOptions<CloudinaryConfig> _config;
    private Cloudinary _cloudinary;

    public UploadManager(IOptions<CloudinaryConfig> config)
    {
        _config = config;
        
        var account = new Account()
        {
            Cloud = _config.Value.CloudName,
            ApiKey = _config.Value.ApiKey,
            ApiSecret = _config.Value.ApiSecret
        };

        _cloudinary = new Cloudinary(account);
    }
    
    public async Task<ImageModel> UploadImage(ImageModel image)
    {
        var file = image.File;
        var result = new ImageUploadResult();

        if (file.Length > 0)
        {
            using (var stream = file.OpenReadStream())
            {
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(file.Name, stream)
                };

                result = _cloudinary.Upload(uploadParams);
            }
        }

        return new ImageModel()
        {
            Url = result.Url.ToString(),
            File = image.File,
            Description = image.Description,
            Timestamp = image.Timestamp,
            Id = result.PublicId,
        };
    }
}