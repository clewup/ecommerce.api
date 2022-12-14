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
        
        CreateMap<CartEntity, CartModel>().ForMember(model => model.Discount, 
            opt => 
                opt.MapFrom(entity => entity.Discount.Code));;
        
        CreateMap<CartModel, CartEntity>().ForMember(entity => entity.Discount.Code, 
            opt => 
                opt.MapFrom(model => model.Discount));;
    }
}