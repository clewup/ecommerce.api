using AutoMapper;
using ecommerce.api.Classes;
using ecommerce.api.Entities;

namespace ecommerce.api.Services.Mappers;

public class OrderMapper : Profile
{
    public OrderMapper()
    {
        CreateMap<OrderEntity, OrderModel>().ReverseMap();
    }
}