using ecommerce.api.Classes;
using ecommerce.api.Entities;

namespace ecommerce.api.Services.Mappers;

public static class ImageMapper
{
    public static ImageModel ToImageModel(this ImageEntity image)
    {
        return new ImageModel()
        {
            Url = image.Url,
        };
    }
}