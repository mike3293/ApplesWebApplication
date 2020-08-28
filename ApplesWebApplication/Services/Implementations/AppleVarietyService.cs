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
    public class AppleVarietyService : IAppleVarietyService
    {
        private readonly DBContext _dbContext;


        public AppleVarietyService(DBContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<IReadOnlyCollection<AppleVariety>> GetAppleVarietiesAsync()
        {
            return await _dbContext.AppleVarieties.ToListAsync()
                .ContinueWith(x => new ReadOnlyCollection<AppleVariety>(x.Result) as IReadOnlyCollection<AppleVariety>);
        }

        public async Task<AppleVariety> GetAppleVarietyAsync(int id)
        {
            var appleVariety = await _dbContext.AppleVarieties.Include(v => v.Regions).FirstOrDefaultAsync(av => av.Id == id);

            return appleVariety;
        }

        public async Task<AppleVariety> CreateAppleVarietyAsync(AppleVariety appleVariety)
        {
            var regions = await _dbContext.Regions.ToListAsync();
            appleVariety.Regions = regions.Where(r => appleVariety.RegionIds.Any(regionId => regionId == r.Id)).ToList();
            appleVariety.RegionIds = null;

            _dbContext.AppleVarieties.Add(appleVariety);
            await _dbContext.SaveChangesAsync();

            return appleVariety;
        }

        public async Task<AppleVariety> UpdateAppleVarietyAsync(int id, AppleVariety appleVariety)
        {
            var regions = await _dbContext.Regions.ToListAsync();
            var newAppleVariety = await _dbContext.AppleVarieties.FindAsync(id);

            newAppleVariety.Name = appleVariety.Name;
            newAppleVariety.AvgWeight = appleVariety.AvgWeight;
            newAppleVariety.Regions = regions.Where(r => appleVariety.RegionIds.Any(regionId => regionId == r.Id)).ToList();

            await _dbContext.SaveChangesAsync();

            return appleVariety;
        }

        public async Task DeleteAppleVarietyAsync(int id)
        {
            AppleVariety appleVariety = new AppleVariety() { Id = id };

            _dbContext.AppleVarieties.Attach(appleVariety);
            _dbContext.AppleVarieties.Remove(appleVariety);

            await _dbContext.SaveChangesAsync();
        }
    }
}