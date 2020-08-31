using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApplesWebApplication.Models
{
    public enum ClimaticZone
    { 
        TropicalZone, 
        Subtropics,
        TemperateZone,
        ColdZone
    }

    public class Region
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public ClimaticZone ClimaticZone { get; set; }

        [NotMapped]
        public int[] AppleVarietyIds { get; set; }

        public ICollection<AppleVariety> AppleVarieties { get; set; }
    }
}