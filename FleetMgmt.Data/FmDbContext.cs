using FleetMgmt.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace FleetMgmt.Data
{
    public class FmDbContext : DbContext
    {
        public FmDbContext(DbContextOptions<FmDbContext> options) : base(options)
        {

        }

        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Trip> Trips { get; set; }
        public DbSet<Accident> Accidents { get; set; }
    }
}
