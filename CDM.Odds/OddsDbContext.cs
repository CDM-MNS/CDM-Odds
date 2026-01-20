using CDM.Odds.Models;
using Microsoft.EntityFrameworkCore;

namespace CDM.Match;

public class OddDbContext : DbContext
{
    public DbSet<OddsEntity> Odds { get; set; }
    
    public OddDbContext(DbContextOptions<OddDbContext> options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<OddsEntity>()
            .HasKey(m => m.OddId);
    }
}