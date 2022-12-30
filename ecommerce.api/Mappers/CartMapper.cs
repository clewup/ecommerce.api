using AutoMapper;
using ecommerce.api.Classes;
using ecommerce.api.Entities;

namespace ecommerce.api.Mappers;

public class CartMapper : Profile
{
    public CartMapper()
    {
        CreateMap<ProductEntity, ProductModel>();
        CreateMap<ProductModel, ProductEntity>();

        CreateMap<CartEntity, CartModel>();
        CreateMap<CartModel, CartEntity>();
    }
}