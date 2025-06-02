using GiftSync.Server.Dtos;
using GiftSync.Server.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GiftSync.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly ILogger<UserController> _logger;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public UserController(ILogger<UserController> logger, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
    {
        _logger = logger;
        _userManager = userManager;
        _signInManager = signInManager;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterUserDto registerUserDto)
    {
        var user = new ApplicationUser
        {
            Name = registerUserDto.Username,
            Email = registerUserDto.Email,
            UserName = registerUserDto.Username,
        };
        
        var result = await _userManager.CreateAsync(user, registerUserDto.Password);

        if (result.Succeeded)
        {
            return Ok("Success");
        }
        
        foreach (var error in result.Errors)
        {
            ModelState.AddModelError("Errors", error.Description);
        }
        
        return BadRequest(ModelState);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto model)
    {
        var result = await _signInManager.PasswordSignInAsync(
            model.Email,
            model.Password,
            isPersistent: false,
            lockoutOnFailure: false
        );

        if (result.Succeeded)
        {
            return Ok("Login successful");
        }

        return Unauthorized("Invalid login attempt.");
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        
        return Ok("Logout successful");
    }

    [HttpDelete("delete-current-user")]
    public async Task<IActionResult> DeleteCurrentUser()
    {
        var user = await _userManager.FindByNameAsync(User.Identity.Name);
        
        if (user == null)
        {
            return Unauthorized();
        }
        
        var result = await _userManager.DeleteAsync(user);

        if (result.Succeeded)
        {
            return Ok("User deleted successful");
        }
        
        foreach (var error in result.Errors)
        {
            ModelState.AddModelError("Errors", error.Description);
        }
        
        return BadRequest(ModelState);
    }
}