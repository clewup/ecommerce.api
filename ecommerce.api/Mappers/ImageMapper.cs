using AutoMapper;
using ecommerce.api.Entities;

namespace ecommerce.api.Mappers;

public static class ImageMapper
{
    public static ImageEntity ToEntity(this string uri)
    {
        return new ImageEntity()
        {
            Url = new Uri(uri),
        };
    }
    
    public static ICollection<ImageEntity> ToEntities(this ICollection<string> uris)
    {
        var images = new List<ImageEntity>();
        
        foreach (var uri in uris)
        {
            images.Add(new ImageEntity()
            {
                Url = new Uri(uri),
            });
        }

        return images;
    }

    public static string ToUri(this ImageEntity image)
    {
        return image.Url.ToString();
    }
    
    public static List<string> ToUris(this ICollection<ImageEntity> images)
    {
        var uris = new List<string>();

        foreach (var image in images)
        {
            uris.Add(image.Url.ToString());
        }

        return uris;
    }
}