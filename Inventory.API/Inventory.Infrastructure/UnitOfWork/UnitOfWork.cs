using Inventory.Application.Interfaces.Repositories;
using Inventory.Domain.Entities;
using Inventory.Infrastructure.DbContext;
using Inventory.Infrastructure.Repositories;

namespace Inventory.Infrastructure.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private InventoryDBContext _context;
    private IInventoryRepository _inventoryRepository;
    private IProductRepository _productRepository;
    public UnitOfWork(InventoryDBContext context, IProductRepository productRepository, IInventoryRepository inventoryRepository) {
        _context = context;
        _productRepository = productRepository;
        _inventoryRepository = inventoryRepository;
        Product = new ProductRepository(_context);
        Inventory = new InventoryRepository(_context);
        InventoryItem = new InventoryItemRepository(_context,_productRepository,_inventoryRepository );
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
        _context.Dispose();
    }
    public int Save() {
        return _context.SaveChanges();
    }

    public async Task<int> SaveAsync(CancellationToken cancellationToken)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }
}