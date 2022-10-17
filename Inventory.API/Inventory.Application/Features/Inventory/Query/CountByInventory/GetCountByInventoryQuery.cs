using AspNetCoreHero.Results;
using AutoMapper;
using Inventory.Application.Interfaces.Repositories;
using MediatR;

namespace Inventory.Application.Features.Inventory.Query.CountByInventory;

    public class GetCountByInventoryQuery : IRequest<Result<IEnumerable<GetCountByInventoryResponse>>>
    {
        public int InventoryId { get; set; }
    
        public class GetCountByInventoryQueryHandler : IRequestHandler<GetCountByInventoryQuery, Result<IEnumerable<GetCountByInventoryResponse>>>
        {
            private readonly IInventoryItemRepository _inventoryItemRepository;
            private readonly IMapper _mapper;

            public GetCountByInventoryQueryHandler(IInventoryItemRepository inventoryItemRepository, IMapper mapper)
            {
                _inventoryItemRepository = inventoryItemRepository;
                _mapper = mapper;
            }

            public async Task<Result<IEnumerable<GetCountByInventoryResponse>>> Handle(GetCountByInventoryQuery query, CancellationToken cancellationToken)
            {
                var count = await _inventoryItemRepository.GetInventoryCountByInventory(query.InventoryId);
                return Result<IEnumerable<GetCountByInventoryResponse>>.Success(count);
            }
        }
    }
