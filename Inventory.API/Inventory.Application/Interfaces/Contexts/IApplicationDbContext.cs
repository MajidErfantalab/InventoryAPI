using System.Data;
using Inventory.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Inventory.Application.Interfaces.Contexts;

public interface IApplicationDbContext
{
    IDbConnection Connection { get; }
    bool HasChanges { get; }

    EntityEntry Entry(object entity);

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);

    DbSet<Domain.Entities.Inventory> Inventories { get; set; }
    DbSet<InventoryItem> InventoryItems { get; set; }
    DbSet<Product> Products { get; set; }
}