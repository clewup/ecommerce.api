using System.Text.Json;
using ecommerce.api.Classes;
using ecommerce.api.Data;
using ecommerce.api.Managers.Interfaces;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;

namespace ecommerce.api.Managers;

public class UploadManager : IUploadManager
{
    static readonly HttpClient _client = new HttpClient();
    private readonly IOptions<FreeImageHostConfig> _config;

    public UploadManager(IOptions<FreeImageHostConfig> config)
    {
        _config = config;
    }
    public async Task<ImageModel> UploadImage(ImageModel image)
    {
        var apiKey = _config.Value.ApiKey;
        var response = await _client.PostAsync($"http://freeimage.host/api/1/upload/?key={apiKey}&source={image.Base64}&format=json", null);
        
        response.EnsureSuccessStatusCode();
        
        var responseString = await response.Content.ReadAsStringAsync();
        var responseJson = JObject.Parse(responseString);

        var url = responseJson["image"]["url"].ToString();
        
        return new ImageModel()
        {
            Base64 = image.Base64,
            Url = url,
        };
    }
}