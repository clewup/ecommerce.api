using AutoMapper;
using ecommerce.api.Classes;
using ecommerce.api.Entities;

namespace ecommerce.api.Services.Mappers;

public class CartMapper : Profile
{
    public CartMapper()
    {
        CreateMap<CartEntity, CartModel>().ReverseMap();
    }
}