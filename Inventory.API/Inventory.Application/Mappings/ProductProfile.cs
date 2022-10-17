using AutoMapper;
using Inventory.Application.DTOs;
using Inventory.Application.Features.Products.Commands.Create;
using Inventory.Domain.Entities;

namespace Inventory.Application.Mappings;

internal class ProductProfile: Profile
{
    public ProductProfile()
    {
        CreateMap<CreateProductCommand, Product>();
    }
}