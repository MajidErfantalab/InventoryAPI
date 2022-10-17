using AspNetCoreHero.Results;
using AutoMapper;
using Inventory.Application.Interfaces.Repositories;
using Inventory.Domain.Entities;
using MediatR;

namespace Inventory.Application.Features.Products.Commands.Create;

public partial class CreateProductCommand  : IRequest<Result<int>>
{
    public long CompanyPrefix { get; set; }
    public string CompanyName { get; set; }
    public long ItemReference { get; set; }
    public string ProductName { get; set; }
}

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Result<int>>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper; 
    private IUnitOfWork _unitOfWork { get; set; }
    
    public CreateProductCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<Result<int>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = _mapper.Map<Product>(request);
        product.Date = DateTime.Now;
        product.IsDeleted = false;
        product.IsEnable = true;
         await _productRepository.InsertAsync(product);
         await _unitOfWork.SaveAsync(cancellationToken);
        return Result<int>.Success(product.Id);
    }
}