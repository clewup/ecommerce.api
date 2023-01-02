using AutoMapper;
using ecommerce.api.Entities;
using ecommerce.api.Models;

namespace ecommerce.api.Mappers;

public static class OrderMapper
{
   public static OrderEntity ToEntity(this OrderModel model)
   {
      return new OrderEntity
      {
         Id = model.Id,
         UserId = model.UserId,
         FirstName = model.FirstName,
         LastName = model.LastName,
         Email = model.Email,
         LineOne = model.DeliveryAddress.LineOne,
         LineTwo = model.DeliveryAddress.LineTwo,
         LineThree = model.DeliveryAddress.LineThree,
         Postcode = model.DeliveryAddress.Postcode,
         City = model.DeliveryAddress.City,
         County = model.DeliveryAddress.County,
         Country = model.DeliveryAddress.Country,
         Total = model.Total,
         DiscountedTotal = model.DiscountedTotal,
         TotalSavings = model.TotalSavings,
         Products = model.Products.ToEntities(),
      };
   }
   
   public static List<OrderEntity> ToEntities(this List<OrderModel> models)
   {
      var orders = new List<OrderEntity>();

      foreach (var model in models)
      {
         orders.Add(new OrderEntity()
         {
            Id = model.Id,
            UserId = model.UserId,
            FirstName = model.FirstName,
            LastName = model.LastName,
            Email = model.Email,
            LineOne = model.DeliveryAddress.LineOne,
            LineTwo = model.DeliveryAddress.LineTwo,
            LineThree = model.DeliveryAddress.LineThree,
            Postcode = model.DeliveryAddress.Postcode,
            City = model.DeliveryAddress.City,
            County = model.DeliveryAddress.County,
            Country = model.DeliveryAddress.Country,
            Total = model.Total,
            DiscountedTotal = model.DiscountedTotal,
            TotalSavings = model.TotalSavings,
            Products = model.Products.ToEntities(),
         });
      }

      return orders;
   }
   
   public static ICollection<OrderEntity> ToEntities(this ICollection<OrderModel> models)
   {
      var orders = new List<OrderEntity>();

      foreach (var model in models)
      {
         orders.Add(new OrderEntity()
         {
            Id = model.Id,
            UserId = model.UserId,
            FirstName = model.FirstName,
            LastName = model.LastName,
            Email = model.Email,
            LineOne = model.DeliveryAddress.LineOne,
            LineTwo = model.DeliveryAddress.LineTwo,
            LineThree = model.DeliveryAddress.LineThree,
            Postcode = model.DeliveryAddress.Postcode,
            City = model.DeliveryAddress.City,
            County = model.DeliveryAddress.County,
            Country = model.DeliveryAddress.Country,
            Total = model.Total,
            DiscountedTotal = model.DiscountedTotal,
            TotalSavings = model.TotalSavings,
            Products = model.Products.ToEntities(),
         });
      }

      return orders;
   }
   
   public static OrderModel ToModel(this OrderEntity entity)
   {
      return new OrderModel()
      {
         Id = entity.Id,
         UserId = entity.UserId,
         FirstName = entity.FirstName,
         LastName = entity.LastName,
         Email = entity.Email,
         DeliveryAddress = new AddressModel()
         {
            LineOne = entity.LineOne,
            LineTwo = entity.LineTwo,
            LineThree = entity.LineThree,
            Postcode = entity.Postcode,
            City = entity.City,
            County = entity.County,
            Country = entity.Country,
         },
         Total = entity.Total,
         DiscountedTotal = entity.DiscountedTotal,
         TotalSavings = entity.TotalSavings,
         Products = entity.Products.ToModels(),
      };
   }
   
   public static List<OrderModel> ToModels(this List<OrderEntity> entities)
   {
      var orders = new List<OrderModel>();

      foreach (var entity in entities)
      {
         orders.Add(new OrderModel()
         {
            Id = entity.Id,
            UserId = entity.UserId,
            FirstName = entity.FirstName,
            LastName = entity.LastName,
            Email = entity.Email,
            DeliveryAddress = new AddressModel()
            {
               LineOne = entity.LineOne,
               LineTwo = entity.LineTwo,
               LineThree = entity.LineThree,
               Postcode = entity.Postcode,
               City = entity.City,
               County = entity.County,
               Country = entity.Country,
            },
            Total = entity.Total,
            DiscountedTotal = entity.DiscountedTotal,
            TotalSavings = entity.TotalSavings,
            Products = entity.Products.ToModels(),
         });
      }

      return orders;
   }
   
   public static ICollection<OrderModel> ToModels(this ICollection<OrderEntity> entities)
   {
      var orders = new List<OrderModel>();

      foreach (var entity in entities)
      {
         orders.Add(new OrderModel()
         {
            Id = entity.Id,
            UserId = entity.UserId,
            FirstName = entity.FirstName,
            LastName = entity.LastName,
            Email = entity.Email,
            DeliveryAddress = new AddressModel()
            {
               LineOne = entity.LineOne,
               LineTwo = entity.LineTwo,
               LineThree = entity.LineThree,
               Postcode = entity.Postcode,
               City = entity.City,
               County = entity.County,
               Country = entity.Country,
            },
            Total = entity.Total,
            DiscountedTotal = entity.DiscountedTotal,
            TotalSavings = entity.TotalSavings,
            Products = entity.Products.ToModels(),
         });
      }

      return orders;
   }
}