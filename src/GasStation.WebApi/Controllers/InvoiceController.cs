using GasStation.Application.Commands.Invoice.Create;
using GasStation.Application.Commands.Invoice.Delete;
using GasStation.Application.Commands.Invoice.Update;
using GasStation.Application.Queries.Invoice.GetAll;
using GasStation.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GasStation.WebApi.Controllers;

[Route("api/invoice")]
public class InvoiceController : ApiController
{
    private readonly ISender _mediator;

    public InvoiceController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("create")]
    public async Task<IActionResult> Create(CreateInvoiceRequest createInvoiceRequest)
    {
        var createInvoiceResponse = await _mediator.Send(createInvoiceRequest);
        
        return createInvoiceResponse.Match(
            response => Ok(response),
            errors => Problem(errors));
    }

    [HttpPut("update")]
    public async Task<IActionResult> Update(UpdateInvoiceRequest updateInvoiceRequest)
    {
        var updateInvoiceResponse = await _mediator.Send(updateInvoiceRequest);
        
        return updateInvoiceResponse.Match(
            response => Ok(response),
            errors => Problem(errors));
    }

    [HttpDelete("delete")]
    public async Task<IActionResult> Delete([FromBody] DeleteInvoiceRequest deleteInvoiceRequest)
    {
        var deleteInvoiceResponse = await _mediator.Send(deleteInvoiceRequest);
        
        return deleteInvoiceResponse.Match(
            response => Ok(response),
            errors => Problem(errors));
    }

    [HttpGet("getAll")]
    public async Task<IActionResult> GetAll()
    {
        var getAllInvoicesResponse = await _mediator.Send(new GetAllInvoicesRequest());

        return Ok(getAllInvoicesResponse);
    }
}