using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ShippingService.Email.Domain.Entities.Identity;
using ShippingService.Email.Persistence.Configs;

namespace ShippingService.Email.Persistence.Context;

public class DataContext : IdentityUserContext<ApplicationUser>
{
    private readonly DatabaseOptions databaseOptions;
    
    public DataContext(DatabaseOptions databaseOptions, DbContextOptions<DataContext> options)
        : base(options)
    {
        this.databaseOptions = databaseOptions;
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasDefaultSchema(databaseOptions.Schema);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
            optionsBuilder.UseNpgsql(databaseOptions.ConnectionString);
    }
}