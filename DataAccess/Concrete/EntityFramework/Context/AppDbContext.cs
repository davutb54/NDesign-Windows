using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework.Context;

public class AppDbContext:DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite($@"Data Source=C:\Users\{Environment.UserName}\AppData\Roaming\JobTracking\app.db");
    }

    public DbSet<Cost> Costs { get; set; }
    public DbSet<Offer> Offers { get; set; }
}