using ApplesWebApplication.Models;
using System.Data.Entity;

namespace ApplesWebApplication.Services
{
    public class DBContext : DbContext
    {
        public DBContext()
        { }

        public DbSet<AppleVariety> AppleVarieties { get; set; }
        public DbSet<Region> Regions { get; set; }
    }
}