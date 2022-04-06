using Microsoft.AspNetCore.Mvc;

namespace BMS.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class RegistrationController : Controller
{
    [HttpPost(Name = "CreateCompanyWithUser")]
    public IActionResult Index()
    {
        return Ok();
    }
}