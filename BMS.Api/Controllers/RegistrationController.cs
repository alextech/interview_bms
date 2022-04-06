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

    [HttpPost(Name = "CreateCompanyWithUser")]
    public async Task<IActionResult> Index()
    {
        CommandResponse<User> commandResponse = await _mediator.Send(new CreateCompanyWithUserCommand("tst", "email", "pass"));

        return Ok(commandResponse.Data);
    }
}