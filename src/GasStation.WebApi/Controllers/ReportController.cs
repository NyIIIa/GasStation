using GasStation.Application.Commands.Report.Create;
using GasStation.Application.Commands.Report.Delete;
using GasStation.Application.Commands.Report.Update;
using GasStation.Application.Queries.Report.GetAll;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GasStation.WebApi.Controllers;

[Route("api/report")]
public class ReportController : ApiController
{
    private readonly ISender _mediator;

    public ReportController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("create")]
    public async Task<IActionResult> Create(CreateReportRequest createReportRequest)
    {
        var createReportResponse = await _mediator.Send(createReportRequest);
        
        return createReportResponse.Match(
            response => Ok(response),
            errors => Problem(errors));
    }

    [HttpPut("update")]
    public async Task<IActionResult> Update(UpdateReportRequest updateReportRequest)
    {
        var updateReportResponse = await _mediator.Send(updateReportRequest);
        
        return updateReportResponse.Match(
            response => Ok(response),
            errors => Problem(errors));
    }

    [HttpDelete("delete")]
    public async Task<IActionResult> Delete(DeleteReportRequest deleteReportRequest)
    {
        var deleteReportResponse = await _mediator.Send(deleteReportRequest);
        
        return deleteReportResponse.Match(
            response => Ok(response),
            errors => Problem(errors));
    }

    [HttpGet("getAll")]
    public async Task<IActionResult> GetAll()
    {
        var getAllReportsResponse = await _mediator.Send(new GetAllReportsRequest());

        return Ok(getAllReportsResponse);
    }
}