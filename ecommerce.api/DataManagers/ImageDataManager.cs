using ecommerce.api.Data;
using ecommerce.api.DataManagers.Contracts;
using ecommerce.api.Entities;
using ecommerce.api.Models;
using Microsoft.EntityFrameworkCore;

namespace ecommerce.api.DataManagers;

public class ImageDataManager : IImageDataManager
{
    private readonly EcommerceDbContext _context;

    public ImageDataManager(EcommerceDbContext context)
    {
        _context = context;
    }

    public async Task<List<ImageEntity>> GetImages(Guid productId)
    {
        var images = await _context.Images
            .Where(i => i.ProductId == productId)
            .ToListAsync();

        return images;
    }
    
    public async Task UploadImage(string imageUrl, ProductEntity product, UserModel user)
    {
        var mappedImage = new ImageEntity()
        {
            Url = new Uri(imageUrl),
            Product = product,
            
            AddedDate = DateTime.UtcNow,
            AddedBy = user.Email,
        };

        await _context.Images.AddAsync(mappedImage);
        await _context.SaveChangesAsync();
    }
}