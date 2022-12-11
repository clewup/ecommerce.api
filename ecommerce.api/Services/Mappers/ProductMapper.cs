using AutoMapper;
using ecommerce.api.Classes;
using ecommerce.api.Entities;

namespace ecommerce.api.Services.Mappers;

public class ProductMapper : Profile
{
    public ProductMapper()
    {
        CreateMap<ProductEntity, ProductModel>();
        CreateMap<ProductModel, ProductEntity>();
    }
}