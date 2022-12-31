using AutoMapper;
using ecommerce.api.Entities;
using ecommerce.api.Models;

namespace ecommerce.api.Mappers;

public class ProductMapper : Profile
{
    public ProductMapper()
    {
        CreateMap<ProductEntity, ProductModel>();
        CreateMap<ProductModel, ProductEntity>();
    }
}