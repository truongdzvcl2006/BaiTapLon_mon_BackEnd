using RealEstateAuction.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RealEstateAuction.Services
{
    public interface IPropertyService
    {
        Task<List<Property>> GetAllPropertiesAsync();
        Task<PropertyDetailViewModel> GetPropertyDetailAsync(int id);
        Task<bool> UpdateViewCountAsync(int id);
    }
}