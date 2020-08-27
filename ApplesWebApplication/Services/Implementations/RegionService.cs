using ApplesWebApplication.Models;
using ApplesWebApplication.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ApplesWebApplication.Services.Implementations
{
    public class RegionService : IRegionService
    {
        private readonly DBContext _dbContext;


        public RegionService(DBContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<IReadOnlyCollection<Region>> GetRegionsAsync()
        {
            return await _dbContext.Regions.Include(r => r.AppleVarieties).ToListAsync()
                .ContinueWith(x => new ReadOnlyCollection<Region>(x.Result) as IReadOnlyCollection<Region>);
        }

        public async Task<Region> GetRegionAsync(int id)
        {
            var region = await _dbContext.Regions.Include(r => r.AppleVarieties).FirstOrDefaultAsync(av => av.Id == id);

            return region;
        }

        public async Task<Region> CreateRegionAsync(Region region)
        {
            var appleVarieties = await _dbContext.AppleVarieties.ToListAsync();
            region.AppleVarieties = appleVarieties.Where(av => region.AppleVarietyIds.Any(appleVarietyId => appleVarietyId == av.Id)).ToList();
            region.AppleVarietyIds = null;

            _dbContext.Regions.Add(region);
            await _dbContext.SaveChangesAsync();

            return region;
        }

        public async Task<Region> UpdateRegionAsync(int id, Region region)
        {
            var appleVarieties = await _dbContext.AppleVarieties.ToListAsync();
            var newRegion = await _dbContext.Regions.FindAsync(id);

            newRegion.Name = region.Name;
            newRegion.ClimaticZone = region.ClimaticZone;
            newRegion.AppleVarieties = appleVarieties.Where(av => region.AppleVarietyIds.Any(appleVarietyId => appleVarietyId == av.Id)).ToList();

            await _dbContext.SaveChangesAsync();

            return region;
        }

        public async Task DeleteRegionAsync(int id)
        {
            Region region = new Region() { Id = id };

            _dbContext.Regions.Attach(region);
            _dbContext.Regions.Remove(region);

            await _dbContext.SaveChangesAsync();
        }
    }
}