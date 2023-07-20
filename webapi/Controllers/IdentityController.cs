using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using HotelDirectory.Models;

namespace webapi.Controllers;

[ApiController]
[Route("/assignroles")]
public class AssignRolesController : ControllerBase
{
    private readonly UserManager<User> userManager;

    public AssignRolesController(UserManager<User> userManager)
    {
        this.userManager = userManager;
    }

    [HttpPost]
    public async Task<IActionResult> AssignRoles([FromBody] AssignRolesRequest request)
    {
        var user = await userManager.FindByNameAsync(request.Username);
        System.Console.WriteLine("User .... ", user);
        if (user == null)
        {
            System.Console.WriteLine("User not found");
            int[] newuser = {};
            return Ok(newuser);
        }
        System.Console.WriteLine("testing");
        await userManager.AddToRoleAsync(user, "role1");
        await userManager.AddToRoleAsync(user, "role2");

        return Ok();
    }
}

public class AssignRolesRequest
{
    public string Username { get; set; }

    public AssignRolesRequest()
    {
        Username = string.Empty;
    }
}

