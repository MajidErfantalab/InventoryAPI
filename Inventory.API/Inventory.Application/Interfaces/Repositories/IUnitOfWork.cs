namespace Inventory.Application.Interfaces.Repositories;

public interface IUnitOfWork : IDisposable
{
    IProductRepository Product {
        get;
    }
    IInventoryRepository Inventory {
        get;
    }
    IInventoryItemRepository InventoryItem {
        get;
    }
    int Save();

    Task<int> SaveAsync(CancellationToken cancellationToken);
}