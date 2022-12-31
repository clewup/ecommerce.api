using AutoMapper;
using ecommerce.api.Entities;
using ecommerce.api.Models;

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