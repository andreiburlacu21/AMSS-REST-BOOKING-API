using AMSS.Rest.Booking.DTO;
using AMSS.Rest.Booking.Service.Authentification;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AMSS.Rest.Booking.Controllers;

[Route("AMSS/[controller]")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    private readonly IServiceAuthentification _authService;

    // private readonly IEmailService _emailService;
    //public AuthenticationController(IAuthenticationService authService, IEmailService emailService = null)
    //{
    //    _authService = authService;
    //    _emailService = emailService;
    //}
    public AuthenticationController(IServiceAuthentification authService)
    {
        _authService = authService;
    }

    // POST api/<AuthController>
    [HttpPost]
    public async Task<IActionResult> GetToken(AuthDto authData)
    {
        try
        {
            return Ok(await _authService.GenerateTokenAsync(authData));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] AccountDto user)
    {
        try
        {
            var result = await _authService.RegisterAsync(user);

            //await _emailService.SendCreatedEmailAsync(result.Email);

            return Ok(true);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("checkLogin")]
    public IActionResult Check()
    {
        try
        {
            var userId = int.Parse(User.FindFirst("Identifier")?.Value);

            if (userId > 0) return Ok(true);
        }
        catch { }

        return Ok(false);

    }
}
