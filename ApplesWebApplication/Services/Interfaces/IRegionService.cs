using ApplesWebApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

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