using AspNetCoreHero.Results;
using AutoMapper;
using Inventory.Application.Extentions;
using Inventory.Application.Interfaces.Repositories;
using Inventory.Domain.Entities;
using MediatR;

namespace Inventory.Application.Features.Inventory.Commands.Create;

public class CreateInventoryCommand : IRequest<Result<int>>
{
    public string InventoryReferenceId { get; set; }
    public string InventoryLocation { get; set; }
    public DateTime DateOfInventory { get; set; }
    public List<string> Tags { get; set; }
}

public class CreateInventoryCommandHandler : IRequestHandler<CreateInventoryCommand, Result<int>>
{
    private readonly IInventoryRepository _inventoryRepository;
    private readonly IInventoryItemRepository _inventoryItemRepository;
    private readonly IMapper _mapper; 
    private IUnitOfWork _unitOfWork { get; set; }

    public CreateInventoryCommandHandler(IInventoryRepository inventoryRepository,IInventoryItemRepository inventoryItemRepository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _inventoryRepository = inventoryRepository;
        _inventoryItemRepository = inventoryItemRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<int>> Handle(CreateInventoryCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var inventory = _mapper.Map<Domain.Entities.Inventory>(request);
            await _inventoryRepository.InsertAsync(inventory);
            foreach (var tag in request.Tags)
            {
                var item = new InventoryItem();
                var tagvalidation = tag.TryParseSgtin96(out EnumHeader header, out EnumFilter filter,
                    out EnumPartition partition,
                    out long companyPrefix, out long itemReference, out long serialNumber, out string errorMessage);
                {
                    var bin = tag.HexToBinary().ToString();
                    item = new InventoryItem()
                    {
                        Date = DateTime.Now,
                        TagBinary = tag.HexToBinary().ToString(),
                        TagHex = tag,
                        InventoryId = inventory.Id,
                        TagCompanyPerfix = companyPrefix != 0 ?  companyPrefix.ToString() : "000010010101111011111101",
                        TagItemReference = itemReference != 0 ?  companyPrefix.ToString() : "11000110010100111101",
                        TagSerialReference = serialNumber != 0 ?  companyPrefix.ToString() : "000010010101111011111101111"
                    };
                }
            
                await _inventoryItemRepository.InsertAsync(item);
            }
            await _unitOfWork.SaveAsync(cancellationToken);
            return Result<int>.Success(inventory.Id);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Result<int>.Fail("Operation Failed!");
        }

    }
}