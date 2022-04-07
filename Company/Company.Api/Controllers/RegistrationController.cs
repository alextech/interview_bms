using System.Net;
using Company.Api.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;

namespace Company.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class RegistrationController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger _logger;

    public RegistrationController(IMediator mediator, ILogger<RegistrationController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> RegisterUserAsync([FromBody] RegisterUserWithCompany registerCommand)
    {
        _logger.LogInformation(
            "----- Sending command: {CommandName}: {@Command}",
            registerCommand.GetGenericTypeName(),
            registerCommand
        );
        bool commandResult = await _mediator.Send(registerCommand);

        if (!commandResult)
        {
            return BadRequest();
        }

        return Ok();
    }
}