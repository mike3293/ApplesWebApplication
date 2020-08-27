using ApplesWebApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ApplesWebApplication.Services.Interfaces
{
    public interface IAppleVarietyService
    {
        Task<IReadOnlyCollection<AppleVariety>> GetAppleVarietiesAsync();

        Task<AppleVariety> GetAppleVarietyAsync(int id);

        Task<AppleVariety> CreateAppleVarietyAsync(AppleVariety appleVariety);

        Task<AppleVariety> UpdateAppleVarietyAsync(int id, AppleVariety appleVariety);

        Task DeleteAppleVarietyAsync(int id);
    }
}