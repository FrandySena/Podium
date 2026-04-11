using Microsoft.AspNetCore.Mvc;
using Podium.Application.Dtos.Auth;
using Podium.Application.Services;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AuthServices _authService;

    public AuthController(AuthServices authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        var result = await _authService.LoginAsync(loginDto);
        if (result.Success)
        {
            return Ok(result);
        }
        return Unauthorized(result);
    }
}