using AspNetCoreHero.Results;
using AutoMapper;
using Inventory.Application.Interfaces.Repositories;
using MediatR;

namespace Inventory.Application.Features.Inventory.Query.CountByCompanyQuery;

public class GetCountByCompanyQuery : IRequest<Result<IEnumerable<GetCountByCompanyResponse>>>
{
    public class GetCountByCompanyQueryHandler : IRequestHandler<GetCountByCompanyQuery, Result<IEnumerable<GetCountByCompanyResponse>>>
    {
        private readonly IInventoryItemRepository _inventoryItemRepository;
        private readonly IMapper _mapper;

        public GetCountByCompanyQueryHandler(IInventoryItemRepository inventoryItemRepository, IMapper mapper)
        {
            _inventoryItemRepository = inventoryItemRepository;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<GetCountByCompanyResponse>>> Handle(GetCountByCompanyQuery query, CancellationToken cancellationToken)
        {
            var count = await _inventoryItemRepository.GetInventoryCountByCompany();
            return Result<IEnumerable<GetCountByCompanyResponse>>.Success(count);
        }
    }
}