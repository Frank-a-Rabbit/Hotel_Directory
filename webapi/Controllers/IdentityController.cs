using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using HotelDirectory.Models;
using System.Security.Claims;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Diagnostics;

namespace webapi.Controllers;

[ApiController]
[Route("/assignroles")]
public class AssignRolesController : ControllerBase
{
    private readonly UserManager<User> userManager;
    private readonly RoleManager<IdentityRole> roleManager;

    public AssignRolesController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
    {
        this.userManager = userManager;
        this.roleManager = roleManager;
    }

    [HttpPost]
    public async Task<IActionResult> AssignRoles([FromBody] AssignRolesRequest request)
    {
        var _existingUser = await userManager.FindByEmailAsync(request.Email);
        System.Console.WriteLine("User already exists: " + _existingUser.GetType() + "!");

        if (_existingUser != null)
        {
            if (request.Email == "jfrankbooth@gmail.com")
            {
                if (!await roleManager.RoleExistsAsync("Administrator"))
                {
                    await roleManager.CreateAsync(new IdentityRole("Administrator"));
                }
                else
                {
                    System.Console.WriteLine("Role Administrator already exists!");
                }
                var addToRoleResult = await userManager.AddToRoleAsync(_existingUser, "Administrator");
                if (!addToRoleResult.Succeeded)
                {
                    return BadRequest(addToRoleResult.Errors);
                } else {
                    System.Console.WriteLine("User " + _existingUser.UserName + " added to role Administrator!");
                }
            }

            string jsonResponse = JsonSerializer.Serialize(_existingUser);
            return Ok(jsonResponse);
        }

        var user = new User
        {
            UserName = request.UserName,
            Email = request.Email
        };

        var result = await userManager.CreateAsync(user, request.Password);
        
        System.Console.WriteLine(request.UserName + " " + request.Email + " " + request.Password + " result " + result);
        if (result.Succeeded)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName)
            };
            System.Console.WriteLine("claimsssss!!!! " + claims);
            await userManager.AddClaimsAsync(user, claims);

            return Ok();
        }
        else
        {
            // Handle user creation failure
            // For example, log the errors or return a meaningful error message
            System.Console.WriteLine(request.UserName + " -- " + request.Email + " " + request.Password);
            return Ok(request);
        }
    }
}

public class AssignRolesRequest
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }

    public AssignRolesRequest()
    {
        UserName = string.Empty;
        Email = string.Empty;
        Password = string.Empty;
    }
}

