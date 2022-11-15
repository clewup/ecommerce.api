using ecommerce.api.Classes;

namespace ecommerce.api.Services.ExtensionMethods;

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
            Password = user.Password,

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

    public static Entities.User ToUserEntity(this User user)
    {
        return new Entities.User()
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            Password = user.Password,

            DeliveryLineOne = user.DeliveryAddress?.LineOne,
            DeliveryLineTwo = user.DeliveryAddress?.LineTwo,
            DeliveryLineThree = user.DeliveryAddress?.LineThree,
            DeliveryPostcode = user.DeliveryAddress?.Postcode,
            DeliveryCity = user.DeliveryAddress?.City,
            DeliveryCounty = user.DeliveryAddress?.County,
            DeliveryCountry = user.DeliveryAddress?.Country,
            DeliveryBuildingNumber = user.DeliveryAddress?.BuildingNumber,
            DeliveryHouseName = user.DeliveryAddress?.HouseName,
            
            BillingLineOne = user.BillingAddress?.LineOne,
            BillingLineTwo = user.BillingAddress?.LineTwo,
            BillingLineThree = user.BillingAddress?.LineThree,
            BillingPostcode = user.BillingAddress?.Postcode,
            BillingCity = user.BillingAddress?.City,
            BillingCounty = user.BillingAddress?.County,
            BillingCountry = user.BillingAddress?.Country,
            BillingBuildingNumber = user.BillingAddress?.BuildingNumber,
            BillingHouseName = user.BillingAddress?.HouseName,
        };
    }
}