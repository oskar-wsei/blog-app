using Microsoft.AspNetCore.Mvc;
using BlogApp.WebAPI.Contracts;
using BlogApp.WebAPI.Domain.Model;
using Microsoft.AspNetCore.Authorization;

namespace BlogApp.WebAPI.Controllers;

[ApiController]
[AllowAnonymous]
[Route("auth")]
public class AuthController(IAuthService authService) : Controller
{
    [HttpGet("{token}")]
    public IActionResult ValidateToken(string token)
    {
        return Ok(new { valid = authService.ValidateToken(token) });
    }

    [HttpPost]
    public IActionResult Authorize([FromBody] AuthRequest user)
    {
        if (!authService.Authorize(user.Username, user.Password)) return Unauthorized();
        return Ok(new { Token = authService.GenerateToken(user.Username) });
    }
}
