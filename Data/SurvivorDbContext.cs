using Microsoft.EntityFrameworkCore;
using Survivor_Api.Entities;

namespace Survivor_Api.Data
{
    public class SurvivorDbContext : DbContext
    {
        public SurvivorDbContext(DbContextOptions<SurvivorDbContext> options) : base(options) 
        {
            
        }

        public DbSet<CategoryEntity> Categories { get; set; }
        public DbSet<CompetitorEntity> Competitors { get; set; }
    }
}
