using ApplesWebApplication.Models;
using ApplesWebApplication.Services;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web.Http;
using ApplesWebApplication.Services.Interfaces;
using System.Threading.Tasks;

namespace ApplesWebApplication.Controllers
{
    public class RegionsController : ApiController
    {
        private readonly IRegionService _regionService;


        public RegionsController(IRegionService regionService)
        {
            _regionService = regionService;
        }


        public async Task<IReadOnlyCollection<Region>> Get()
        {
            var regions = await _regionService.GetRegionsAsync();

            return regions;
        }

        public async Task<IHttpActionResult> GetById(int id)
        {
            var region = await _regionService.GetRegionAsync(id);
            if (region == null)
            {
                return NotFound();
            }

            return Ok(region);
        }

        public async Task<IHttpActionResult> Post([FromBody] Region region)
        {
            if(region.Name == null)
            {
                return BadRequest();
            }

            var createdRegion = await _regionService.CreateRegionAsync(region);

            return Ok(createdRegion);
        }

        // TODO: validation for not null
        public async Task<IHttpActionResult> Put(int id, Region region)
        {
            if (region.Name == null)
            {
                return BadRequest();
            }

            await _regionService.UpdateRegionAsync(id, region);

            return Ok();
        }

        public async Task<IHttpActionResult> Delete(int id)
        {
            await _regionService.DeleteRegionAsync(id);

            return Ok();
        }
    }
}
