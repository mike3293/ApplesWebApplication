using ApplesWebApplication.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

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