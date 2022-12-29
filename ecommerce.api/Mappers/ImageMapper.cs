using AutoMapper;
using ecommerce.api.Classes;
using ecommerce.api.Entities;

namespace ecommerce.api.Mappers;

public class ImageMapper : Profile
{
    public ImageMapper()
    {
        CreateMap<ImageEntity, ImageModel>();
        CreateMap<ImageModel, ImageEntity>();
    }
}