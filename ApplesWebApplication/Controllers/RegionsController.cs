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
    [RoutePrefix("api/appleRegions")]
    public class RegionsController : ApiController
    {
        private readonly IRegionService _regionService;


        public RegionsController(IRegionService regionService)
        {
            _regionService = regionService;
        }


        [Route("")]
        public async Task<IReadOnlyCollection<Region>> Get()
        {
            var regions = await _regionService.GetRegionsAsync();

            return regions;
        }

        [Route("{id:int}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetById(int id)
        {
            var region = await _regionService.GetRegionAsync(id);
            if (region == null)
            {
                return NotFound();
            }

            return Ok(region);
        }

        [Route("")]
        [HttpPost]
        public async Task<IHttpActionResult> CreateRegion([FromBody] Region region)
        {
            var createdRegion = await _regionService.CreateRegionAsync(region);

            return Ok(createdRegion);
        }

        [Route("{id:int}")]
        public async Task<IHttpActionResult> Put(int id, Region region)
        {
            await _regionService.UpdateRegionAsync(id, region);

            return Ok();
        }

        [Route("{id:int}")]
        public async Task<IHttpActionResult> Delete(int id)
        {
            await _regionService.DeleteRegionAsync(id);

            return Ok();
        }
    }
}
