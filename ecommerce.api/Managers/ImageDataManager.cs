using ecommerce.api.Classes;
using ecommerce.api.Data;
using ecommerce.api.Entities;
using Microsoft.EntityFrameworkCore;

namespace ecommerce.api.Managers;

public class ImageDataManager
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
    
    public async Task UploadImage(ImageModel image, Guid productId)
    {
        var mappedImage = new ImageEntity()
        {
            Url = image.Url,
            ProductId = productId,
        };

        await _context.Images.AddAsync(mappedImage);
        await _context.SaveChangesAsync();
    }
}