using BMS.Company.Cmd;
using BMS.Company.Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;

namespace BMS.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class RegistrationController : Controller
{
    private readonly IMediator _mediator;

    public RegistrationController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /*
     * Alternatively could use
     *
     * public async Task<IActionResult> Index([FromForm] CreateCompanyWithUserCommand createCommand)
     *
     * to hydrate command automatically, but it would require CreateCompanyCommand to have parameterless constructor
     * and all properties publicly mutable.
     * This would require more validation on handler side, instead of relying on compiler
     */

    [HttpPost(Name = "CreateCompanyWithUser")]
    public async Task<IActionResult> Index(
        [FromForm] string companyName,
        [FromForm] string userEmail, [FromForm] string password)
    {
        CommandResponse<User> commandResponse = await _mediator.Send(
            new CreateCompanyWithUserCommand(companyName, userEmail, password)
        );

        if (!commandResponse.Success)
        {
            return Problem(commandResponse.Description, null, StatusCodes.Status409Conflict);
        }

        // shows that data was indeed saved.
        // Usually can just give it full instance of commandResponse, but json serializer will trip over cyclical
        // relationship between company
        return Ok( new {
            userEmail = commandResponse.Data?.Email,
            userId = commandResponse.Data?.Guid,
            companyId = commandResponse.Data?.Guid
        });
    }
}