using GasStation.Application.Commands.User.Register;
using GasStation.Application.Queries.User.Login;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GasStation.WebApi.Controllers;


[Microsoft.AspNetCore.Components.Route("auth")]
[AllowAnonymous]
public class AuthenticationController : ApiController
{
    private readonly IMediator _mediator;

    public AuthenticationController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterUserRequest registerUserRequest)
    {
        var registerResponse = await _mediator.Send(registerUserRequest);

       return registerResponse.Match(
           response => Ok(response),
           errors => Problem(errors));
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginUserRequest loginUserRequest)
    {
        var loginResponse = await _mediator.Send(loginUserRequest);
        
        return loginResponse.Match(
            response => Ok(response),
            errors => Problem(errors));
    }
}