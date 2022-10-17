using AspNetCoreHero.Results;
using AutoMapper;
using Inventory.Application.Interfaces.Repositories;
using MediatR;

namespace Inventory.Application.Features.Inventory.Query.CountPerDayQuery;

public class GetCountPerDayQuery : IRequest<Result<IEnumerable<GetCountPerDayResponse>>>
{
    public class GetCountPerDayQueryHandler : IRequestHandler<GetCountPerDayQuery, Result<IEnumerable<GetCountPerDayResponse>>>
    {
        private readonly IInventoryItemRepository _inventoryItemRepository;
        private readonly IMapper _mapper;

        public GetCountPerDayQueryHandler(IInventoryItemRepository inventoryItemRepository, IMapper mapper)
        {
            _inventoryItemRepository = inventoryItemRepository;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<GetCountPerDayResponse>>> Handle(GetCountPerDayQuery query, CancellationToken cancellationToken)
        {
            var Count = await _inventoryItemRepository.GetInventoryCountPerDay();
            return Result<IEnumerable<GetCountPerDayResponse>>.Success(Count);
        }
    }
}