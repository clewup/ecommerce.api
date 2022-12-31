using AutoMapper;
using ecommerce.api.Entities;
using ecommerce.api.Models;

namespace ecommerce.api.Mappers;

public class OrderMapper : Profile
{
    public OrderMapper()
    {
        CreateMap<OrderEntity, OrderModel>()
            .ForMember(model => model.OrderDate, 
                opt => 
                    opt.MapFrom(entity => entity.AddedDate));
        
        CreateMap<OrderModel, OrderEntity>()
            .ForMember(entity => entity.AddedDate, 
                opt => 
                    opt.MapFrom(model => model.OrderDate));
    }
}