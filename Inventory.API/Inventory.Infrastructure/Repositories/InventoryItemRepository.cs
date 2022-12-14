using Inventory.Application.Features.Inventory.Query.CountByCompanyQuery;
using Inventory.Application.Features.Inventory.Query.CountByInventory;
using Inventory.Application.Features.Inventory.Query.CountPerDayQuery;
using Inventory.Application.Interfaces.Repositories;
using Inventory.Domain.Entities;
using Inventory.Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Infrastructure.Repositories;

public class InventoryItemRepository : GenericRepository < InventoryItem > , IInventoryItemRepository
{
    private readonly IProductRepository _productRepository;
    private readonly IInventoryRepository _inventoryRepository;
    public InventoryItemRepository(InventoryDBContext context, IProductRepository productRepository, IInventoryRepository inventoryRepository)
            :base(context)
    {
        _productRepository = productRepository;
        _inventoryRepository = inventoryRepository;
    }

    public async Task<IEnumerable<GetCountByCompanyResponse>> GetInventoryCountByCompany()
    {
        var query = context.InventoryItems
            .GroupBy(x => new { Type = x.TagCompanyPerfix })
            .Select(x => new
                { Count = x.Count(), Type = x.Key.Type })
            .AsEnumerable()
            .Select(x => new GetCountByCompanyResponse
                { Count = x.Count, CompanyPrefix = x.Type });

        //If the Company name is not needed
        return query.ToList();
        
        // If the company name is important I would add a new entity for the company so we could use that in other places. 
        // We also have the company name in the product entity and I could add a navigation property or get it from the repository 
        // I just don't have more time to put on this.
    }

    public async Task<IEnumerable<GetCountByInventoryResponse>> GetInventoryCountByInventory(int queryInventoryId)
    {
        var query = context.InventoryItems
            .Where(x => x.InventoryId == queryInventoryId)
            .GroupBy(x => new { Type = x.TagItemReference })
            .Select(x => new
                { Count = x.Count(), Type = x.Key.Type })
            .AsEnumerable()
            .Select(x => new GetCountByInventoryResponse
                { Count = x.Count, Product = x.Type });
        
        //If the Product name is not needed
        return query.ToList();
        
        // If the product name is important I could add a navigation key to get the product from the TagItemReference 
        // Or even use the repository of product to get the name 
        // I just don't have more time to put on this.
    }

    public async Task<IEnumerable<GetCountPerDayResponse>> GetInventoryCountPerDay()
    {
        return context.InventoryItems
            .GroupBy(x => new { Year = x.Date.Year, Month = x.Date.Month, Day = x.Date.Day })
            .Select(x => new
                { Year = x.Key.Year, Month = x.Key.Month, Day = x.Key.Day, Count = x.Count() })
            .AsEnumerable()
            .Select(x => new GetCountPerDayResponse
                { Date = new DateTime(x.Year, x.Month, x.Day).ToString("dd/MM/yyyy"), Count = x.Count})
            .ToList();
    }
}