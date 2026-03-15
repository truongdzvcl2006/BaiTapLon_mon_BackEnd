using Microsoft.EntityFrameworkCore;
using RealEstateAuction.Data;
using RealEstateAuction.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstateAuction.Services
{
    public class PropertyService : IPropertyService
    {
        private readonly AppDbContext _context;

        public PropertyService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Property>> GetAllPropertiesAsync()
        {
            return await _context.Properties.ToListAsync();
        }

        public async Task<PropertyDetailViewModel> GetPropertyDetailAsync(int id)
        {
            var property = await _context.Properties
                .Include(p => p.PropertyImages)
                .Include(p => p.PropertyAmenities)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (property == null) return null;

            return new PropertyDetailViewModel
            {
                Id = property.Id,
                Title = property.Title,
                Price = property.Price,
                Location = property.Location,
                Description = property.Description,
                Area = property.Area,
                Bedrooms = property.Bedrooms,
                Bathrooms = property.Bathrooms,
                PropertyType = property.PropertyType,
                MainImageUrl = property.ImageUrl,
                GalleryImages = property.PropertyImages?.Select(img => img.ImageUrl).ToList(),
                Amenities = property.PropertyAmenities?.Select(a => a.AmenityName).ToList(),
                IsHot = property.IsHot
            };
        }

        public async Task<bool> UpdateViewCountAsync(int id)
        {
            var property = await _context.Properties.FindAsync(id);
            if (property != null)
            {
                // Update view count logic here
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}