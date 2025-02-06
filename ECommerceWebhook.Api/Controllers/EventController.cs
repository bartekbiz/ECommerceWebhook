using ECommerceWebhook.Api.DTOs;
using ECommerceWebhook.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceWebhook.Api.Controllers;

[ApiController]
[Route("events")]
public class EventController : ControllerBase
{
    private readonly IEventService _eventService;

    public EventController(IEventService eventService)
    {
        _eventService = eventService;
    }

    [HttpPost]
    public async Task<IActionResult> HandleEvent([FromBody] EventRequestDTO requestDto)
    {
        await _eventService.ProcessEventAsync(requestDto.EventName, requestDto.OrderNumber);
        return StatusCode(StatusCodes.Status200OK);
    }
}