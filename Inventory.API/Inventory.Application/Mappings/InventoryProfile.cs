using AutoMapper;
using Inventory.Application.Features.Inventory.Commands.Create;
using Inventory.Domain.Entities;

namespace Inventory.Application.Mappings;

public class InventoryProfile: Profile
{
    public InventoryProfile()
    {
        CreateMap<CreateInventoryCommand, Domain.Entities.Inventory>();
    }
}