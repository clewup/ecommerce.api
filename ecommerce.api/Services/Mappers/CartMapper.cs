using AutoMapper;
using ecommerce.api.Classes;
using ecommerce.api.Entities;

namespace ecommerce.api.Services.Mappers;

public class CartMapper : Profile
{
    public CartMapper()
    {
        CreateMap<ProductEntity, ProductModel>();
        CreateMap<ProductModel, ProductEntity>();
        
        CreateMap<DiscountEntity, DiscountModel>();
        CreateMap<DiscountModel, DiscountEntity>();
        
        CreateMap<CartEntity, CartModel>();
        CreateMap<CartModel, CartEntity>();
    }
}