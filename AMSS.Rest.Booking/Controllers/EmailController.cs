using AMSS.Rest.Booking.DTO;
using AMSS.Rest.Booking.Service.Model.Contracts;
using AMSS.Rest.Booking.Services.Email;
using AMSS.Rest.Booking.Utils.Enums;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AMSS.Rest.Booking.Controllers;

[Route("AMSS/[controller]")]
[ApiController]
[Authorize]
public class EmailController : ControllerBase
{
    IServiceEmail _emailService;

    public EmailController(IServiceEmail emailService)
    {
        _emailService = emailService;
    }

    [HttpPost("booking")]
    [Authorize(Roles = "Admin,User")]
    public async Task<IActionResult> Appointment([FromBody] BookingDto booking)
    {
        try
        {
            var userId = int.Parse(User.FindFirst("Identifier")?.Value);
            await _emailService.SendRentMadeEmailAsync(userId, booking);
            return Ok(true);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    [HttpGet("finished")]
    [Authorize(Roles = "Admin,User")]

    public async Task<IActionResult> Finished()
    {
        try
        {
            var userId = int.Parse(User.FindFirst("Identifier")?.Value);
            await _emailService.SendRentFinishedEmailAsync(userId);
            return Ok(true);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost("confirmationBooking/{key}")]
    [Authorize(Roles = "Admin,User")]

    public async Task<IActionResult> ConfirmationBooking(string key, [FromBody] BookingDto booking)
    {
        try
        {
            var userId = int.Parse(User.FindFirst("Identifier")?.Value);
            await _emailService.ConfirmBooking(userId, key, booking);
            return Ok(true);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
