using GasStation.Application.Commands.Fuel.Create;
using GasStation.Application.Commands.Fuel.Update;
using GasStation.Application.Queries.Fuel.GetAll;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GasStation.WebApi.Controllers;

[Route("api/fuel")]
public class FuelController : ApiController
{
    private readonly ISender _mediator;

    public FuelController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("create")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create([FromBody] CreateFuelRequest createFuelRequest)
    {
        var createFuelResponse = await _mediator.Send(createFuelRequest);

        return createFuelResponse.Match(
            response => Ok(response),
            errors => Problem(errors));
    }

    [HttpPut("update")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update([FromBody] UpdateFuelRequest updateFuelRequest)
    {
        var updateFuelResponse = await _mediator.Send(updateFuelRequest);
        
        return updateFuelResponse.Match(
            response => Ok(response),
            errors => Problem(errors));
    }

    [HttpGet("getAll")]
    public async Task<IActionResult> GetAll()
    {
        var getAllFuelsResponse = await _mediator.Send(new GetAllFuelsRequest());

        return Ok(getAllFuelsResponse);
    }
}