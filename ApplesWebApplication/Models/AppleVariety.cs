using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ApplesWebApplication.Models
{
    public class AppleVariety
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public double AvgWeight { get; set; }

        [NotMapped]
        public int[] RegionIds { get; set; }

        public ICollection<Region> Regions { get; set; }
    }
}