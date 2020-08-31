using ApplesWebApplication.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

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