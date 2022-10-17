using AutoMapper;
using Inventory.Application.Features.Products.Commands.Create;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.API.Controllers;

public class ProductController : BaseController<ProductController>
{
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Post(CreateProductCommand command)
    {
        return Ok(await _mediator.Send(command)); 
    }
}