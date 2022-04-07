using System.Net;
using Company.Api.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Company.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class RegistrationController : ControllerBase
{
    private readonly IMediator _mediator;

    public RegistrationController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> RegisterUserAsync([FromBody] RegisterUserWithCompany registerCommand)
    {
        bool commandResult = await _mediator.Send(registerCommand);

        if (!commandResult)
        {
            return BadRequest();
        }

        return Ok();
    }
}