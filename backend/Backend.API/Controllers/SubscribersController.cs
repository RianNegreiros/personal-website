using Backend.Application.Models.InputModels;
using Backend.Application.Services.Interfaces;

using Microsoft.AspNetCore.Mvc;

namespace Backend.API.Controllers;

public class SubscribersController : BaseApiController
{
    private readonly ISubscriberService _subscriberService;

    public SubscribersController(ISubscriberService subscriberService)
    {
        _subscriberService = subscriberService;
    }

    [HttpPost("subscribe")]
    public async Task<IActionResult> Subscribe([FromBody] SubscriberInputModel request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _subscriberService.AddSubscriberAsync(request.Email);
        return !result ? BadRequest("Email is already subscribed.") : Ok("Subscribed successfully.");
    }

    [HttpGet("unsubscribe")]
    public async Task<IActionResult> Unsubscribe([FromQuery] string email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            return BadRequest("Invalid email address.");
        }

        var result = await _subscriberService.UnsubscribeAsync(email);
        return !result ? BadRequest("Email was not subscribed or could not be found.") : Ok("Unsubscribed successfully.");
    }
}
