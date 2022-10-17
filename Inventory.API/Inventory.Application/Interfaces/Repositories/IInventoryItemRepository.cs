using Inventory.Application.Features.Inventory.Query.CountByCompanyQuery;
using Inventory.Application.Features.Inventory.Query.CountByInventory;
using Inventory.Application.Features.Inventory.Query.CountPerDayQuery;
using Inventory.Domain.Entities;

namespace Inventory.Application.Interfaces.Repositories;

public interface IInventoryItemRepository : IGenericRepository < InventoryItem > 
{
    Task<IEnumerable<GetCountByCompanyResponse>> GetInventoryCountByCompany();
    Task<IEnumerable<GetCountByInventoryResponse>> GetInventoryCountByInventory(int queryInventoryId);
    Task<IEnumerable<GetCountPerDayResponse>> GetInventoryCountPerDay();
}