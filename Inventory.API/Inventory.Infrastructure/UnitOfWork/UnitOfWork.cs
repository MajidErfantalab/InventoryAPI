using Inventory.Application.Interfaces.Repositories;
using Inventory.Domain.Entities;
using Inventory.Infrastructure.DbContext;
using Inventory.Infrastructure.Repositories;

namespace Inventory.Infrastructure.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private InventoryDBContext context;
    public UnitOfWork(InventoryDBContext context) {
        this.context = context;
        Product = new ProductRepository(this.context);
        Inventory = new InventoryRepository(this.context);
        InventoryItem = new InventoryItemRepository(this.context);
    }
    public IProductRepository Product {
        get;
        private set;
    }
    public IInventoryRepository Inventory {
        get;
        private set;
    }
    public IInventoryItemRepository InventoryItem {
        get;
        private set;
    }
    public void Dispose() {
        context.Dispose();
    }
    public int Save() {
        return context.SaveChanges();
    }

    public async Task<int> SaveAsync(CancellationToken cancellationToken)
    {
        return await context.SaveChangesAsync(cancellationToken);
    }
}