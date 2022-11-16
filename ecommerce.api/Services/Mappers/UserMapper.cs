using ecommerce.api.Classes;

namespace ecommerce.api.Services.Mappers;

public static class UserMapper
{
    public static User ToUserModel(this Entities.User user)
    {
        return new User()
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,

            DeliveryAddress = new Address()
            {
                LineOne = user.DeliveryLineOne,
                LineTwo = user.DeliveryLineTwo,
                LineThree = user.DeliveryLineThree,
                Postcode = user.DeliveryPostcode,
                City = user.DeliveryCity,
                County = user.DeliveryCounty,
                Country = user.DeliveryCountry,
                BuildingNumber = user.DeliveryBuildingNumber,
                HouseName = user.DeliveryHouseName
            },
            
            BillingAddress = new Address()
            {
                LineOne = user.BillingLineOne,
                LineTwo = user.BillingLineTwo,
                LineThree = user.BillingLineThree,
                Postcode = user.BillingPostcode,
                City = user.BillingCity,
                County = user.BillingCounty,
                Country = user.BillingCountry,
                BuildingNumber = user.BillingBuildingNumber,
                HouseName = user.BillingHouseName
            }
        };
    }
}