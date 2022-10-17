using Inventory.Application.Interfaces.Repositories;
using Inventory.Infrastructure.DbContext;

namespace Inventory.Infrastructure.Repositories;

public class InventoryRepository: GenericRepository < Domain.Entities.Inventory > , IInventoryRepository
{
    public InventoryRepository(InventoryDBContext context)
        :base(context){}
}