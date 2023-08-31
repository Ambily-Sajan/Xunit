using Microsoft.EntityFrameworkCore;
using Xunit_API.Models;

namespace Xunit_API.Data
{
    public class VehicleDbContext:DbContext
    {
        public VehicleDbContext(DbContextOptions options):base(options) 
        { 

        }
        public DbSet<brand>vehicle_brands { get; set; }
        public DbSet<Model>vehicle_model { get; set; }
        public DbSet<VehicleType> vehicle_type { get; set; } 
        
    }
}
