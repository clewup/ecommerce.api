using AutoMapper;
using ecommerce.api.Classes;
using ecommerce.api.Entities;

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