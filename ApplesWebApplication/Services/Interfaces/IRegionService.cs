using ApplesWebApplication.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplesWebApplication.Services.Interfaces
{
    public interface IRegionService
    {
        Task<IReadOnlyCollection<Region>> GetRegionsAsync();

        Task<Region> GetRegionAsync(int id);

        Task<Region> CreateRegionAsync(Region region);

        Task<Region> UpdateRegionAsync(int id, Region region);

        Task DeleteRegionAsync(int id);
    }
}