using Inventory.Application.Features.Inventory.Commands.Create;
using Inventory.Application.Features.Products.Commands.Create;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.API.Controllers;

public class InventoryController : BaseController<InventoryController>
{
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Post(CreateInventoryCommand command)
    {
        return Ok(await _mediator.Send(command)); 
    }
}