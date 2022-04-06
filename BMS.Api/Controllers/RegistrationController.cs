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
     * This would require more validation on handler side, instead of relying on compiler.
     *
     * Or, can create NewCompanyDTO and NewUserDTO to group fields, but would require ensuring structured JSON object
     * coming from API consumer.
     */

    [HttpPost(Name = "CreateCompanyWithUser")]
    public async Task<IActionResult> Index(
        [FromForm] Guid companyId,
        [FromForm] string companyName,
        [FromForm] Guid userId,
        [FromForm] string userEmail, [FromForm] string password)
    {
        CommandResponse<User> commandResponse = await _mediator.Send(
            new CreateCompanyWithUserCommand(
                Guid.NewGuid(),
                companyName,
                Guid.NewGuid(),
                userEmail, password)
        );

        if (!commandResponse.Success)
        {
            // should inspect type of return state, to see if its system failure or input error.
            // system failure should return 500
            return Problem(commandResponse.Description, null, StatusCodes.Status409Conflict);
        }

        // can return data to show it was indeed inserted
        // often can give full object, but no need to expose password, even hashed
        // would be less of problem, if password identity management was offloaded to IdentityFramework
        return Ok( new {
            userEmail = commandResponse.Data?.Email,
            userId = commandResponse.Data?.Guid,
            companyId = commandResponse.Data?.Guid
        });
    }
}