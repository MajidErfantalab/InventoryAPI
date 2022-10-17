using Inventory.Application.Features.Inventory.Commands.Create;
using Inventory.Application.Features.Inventory.Query.CountByCompanyQuery;
using Inventory.Application.Features.Inventory.Query.CountByInventory;
using Inventory.Application.Features.Inventory.Query.CountPerDayQuery;
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
    
    [HttpGet]
    [Route("GetCountPerProductByInventory")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetCountByInventory(int inventoryId)
    {
        return Ok(await _mediator.Send(new GetCountByInventoryQuery() { InventoryId = inventoryId})); 
    }
    
    [HttpGet]
    [Route("GetCountPerCompany")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetCountByCompany()
    {
        return Ok(await _mediator.Send(new GetCountByCompanyQuery())); 
    }
    
    [HttpGet]
    [Route("GetCountPerDay")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetCountByDay()
    {
        return Ok(await _mediator.Send(new GetCountPerDayQuery())); 
    }
}