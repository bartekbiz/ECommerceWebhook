using AutoMapper;
using ECommerceWebhook.Application.Services;
using ECommerceWebhook.Domain.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceWebhook.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class EventsController : ControllerBase
{
    private readonly IEventsService _eventsService;

    public EventsController(IEventsService eventsService)
    {
        _eventsService = eventsService;
    }

    [HttpPost]
    public async Task<IActionResult> HandleAsync([FromBody] EventRequestDto eventRequestDto)
    {
        await _eventsService.HandleAsync(eventRequestDto.EventName, eventRequestDto.OrderNumber);
        return StatusCode(StatusCodes.Status200OK);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var events = await _eventsService.GetAllAsync();
        return StatusCode(StatusCodes.Status200OK, events);
    }
}