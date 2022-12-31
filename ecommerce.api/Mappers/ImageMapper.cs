using AutoMapper;
using ecommerce.api.Entities;

namespace ecommerce.api.Mappers;

public class ImageMapper : Profile
{
    public ImageMapper()
    {
        CreateMap<ImageEntity, String>().ConvertUsing(i => i.Url.ToString());
        
        CreateMap<String, ImageEntity>().ForMember(entity => entity.Url, 
            opt => 
                opt.MapFrom(src => src));
    }
}