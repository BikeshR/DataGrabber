using DataGrabber.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataGrabber.Data
{
    public class DailyPriceContext : DbContext
    {
        public DbSet<DailyPrice> DailyPrices { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Server = DESKTOP-JOF9B2G; Database = Securities_Master; Trusted_Connection = True;");
            base.OnConfiguring(optionsBuilder);
        }
    }
}
