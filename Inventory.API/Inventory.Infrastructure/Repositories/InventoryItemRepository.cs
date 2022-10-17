using Inventory.Application.Interfaces.Repositories;
using Inventory.Domain.Entities;
using Inventory.Infrastructure.DbContext;

namespace Inventory.Infrastructure.Repositories;

public class InventoryItemRepository : GenericRepository < InventoryItem > , IInventoryItemRepository
{
    public InventoryItemRepository(InventoryDBContext context)
            :base(context){}
}