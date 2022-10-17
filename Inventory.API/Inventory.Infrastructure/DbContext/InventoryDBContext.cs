using Inventory.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Inventory.Infrastructure.DbContext;

public class InventoryDBContext : Microsoft.EntityFrameworkCore.DbContext
{
    public InventoryDBContext(DbContextOptions<InventoryDBContext> options): base(options)
    {
            
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase(databaseName: "InventoryDB");
    }
    
    public DbSet<Domain.Entities.Inventory>? Inventories { get; set; }
    public DbSet<InventoryItem>? InventoryItems { get; set; }
    public DbSet<Product>? Products { get; set; }
    
}