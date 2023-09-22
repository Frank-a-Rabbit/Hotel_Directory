using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using HotelDirectory.Models;
using System.Security.Claims;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Diagnostics;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Net.Sockets;

namespace webapi.Controllers;

[ApiController]
[Route("/assignroles")]
public class AssignRolesController : ControllerBase
{
    private readonly UserManager<User> userManager;
    private readonly RoleManager<IdentityRole> roleManager;
    private readonly IPasswordHasher<User> passwordHasher; 

    public AssignRolesController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IPasswordHasher<User> passwordHasher)
    {
        this.userManager = userManager;
        this.roleManager = roleManager;
        this.passwordHasher = passwordHasher;
    }

    [HttpPost]
    public async Task<IActionResult> AssignRoles([FromBody] AssignRolesRequest request)
    {
        var _existingUser = await userManager.FindByEmailAsync(request.Email);

        if (_existingUser != null)
        {
            System.Console.WriteLine("User " + _existingUser.PasswordHash + " already exists! ");
            var passwordCheck = await userManager.CheckPasswordAsync(_existingUser, request.Password);
            System.Console.WriteLine("Existing user: " + _existingUser);
            System.Console.WriteLine("Password check: " + passwordCheck);
            if (!passwordCheck)
            {
                return BadRequest("Invalid password!!");
            }
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

        if (result.Succeeded)
        {
            System.Console.WriteLine("User " + user.UserName + " created!");
            if (!await roleManager.RoleExistsAsync("Administrator"))
            {
                await roleManager.CreateAsync(new IdentityRole("Administrator"));
            }
            else
            {
                System.Console.WriteLine("Role Administrator already exists!");
                return BadRequest("Role Administrator already exists!");
            }
            if (request.Email == "jfrankbooth@gmail.com")
            {
                var addToRoleResult = await userManager.AddToRoleAsync(user, "Administrator");
                if (!addToRoleResult.Succeeded)
                {
                    return BadRequest(addToRoleResult.Errors);
                } else {
                    System.Console.WriteLine("User " + user.UserName + " added to role Administrator!");
                }
            }
            else 
            {
                var addToRoleResult = await userManager.AddToRoleAsync(user, "User");
                if (!addToRoleResult.Succeeded)
                {
                    return BadRequest(addToRoleResult.Errors);
                } else {
                    System.Console.WriteLine("User " + user.UserName + " added to role User!");
                }
            }
            return Ok(user);
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

