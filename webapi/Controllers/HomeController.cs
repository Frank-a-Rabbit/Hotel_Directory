using Microsoft.AspNetCore.Mvc;

namespace webapi.Controllers;

[ApiController]
[Route("/")]

public class HomeController : ControllerBase
{
    [HttpGet]
    public IActionResult Index() {
        return Content("Welcome home, buddy!", "text/plain");
    }
}
