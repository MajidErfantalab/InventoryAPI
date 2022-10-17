using Inventory.Application.Interfaces.Repositories;
using Inventory.Domain.Entities;
using Inventory.Infrastructure.DbContext;

namespace Inventory.Infrastructure.Repositories;

public class ProductRepository: GenericRepository < Product > , IProductRepository
{
    public ProductRepository(InventoryDBContext context)
        :base(context){}
}