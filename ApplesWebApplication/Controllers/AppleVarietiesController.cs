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
    public class AppleVarietiesController : ApiController
    {
        private readonly IAppleVarietyService _appleVarietyService;


        public AppleVarietiesController(IAppleVarietyService appleVarietyService)
        {
            _appleVarietyService = appleVarietyService;
        }


        public async Task<IReadOnlyCollection<AppleVariety>> Get()
        {
            var appleVarieties = await _appleVarietyService.GetAppleVarietiesAsync();

            return appleVarieties;
        }

        public async Task<IHttpActionResult> GetById(int id)
        {
            var appleVariety = await _appleVarietyService.GetAppleVarietyAsync(id);
            if (appleVariety == null)
            {
                return NotFound();
            }

            return Ok(appleVariety);
        }

        public async Task<IHttpActionResult> Post([FromBody] AppleVariety appleVariety)
        {
            var createdAppleVariety = await _appleVarietyService.CreateAppleVarietyAsync(appleVariety);

            return Ok(createdAppleVariety);
        }

        // TODO: validation for not null
        public async Task<IHttpActionResult> Put(int id, AppleVariety appleVariety)
        {
            await _appleVarietyService.UpdateAppleVarietyAsync(id, appleVariety);

            return Ok();
        }

        public async Task<IHttpActionResult> Delete(int id)
        {
            await _appleVarietyService.DeleteAppleVarietyAsync(id);

            return Ok();
        }
    }
}
