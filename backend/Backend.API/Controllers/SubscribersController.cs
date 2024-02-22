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
}
