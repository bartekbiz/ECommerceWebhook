using ECommerceWebhook.Application.Services;
using ECommerceWebhook.Domain.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceWebhook.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class WebhooksController : ControllerBase
{
    private readonly IWebhooksService _webhooksService;

    public WebhooksController(IWebhooksService webhooksService)
    {
        _webhooksService = webhooksService;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var webhooks = await _webhooksService.GetAllAsync();
        return StatusCode(StatusCodes.Status200OK, webhooks);
    }

    [HttpPost]
    public async Task<IActionResult> AddAsync([FromBody] WebhookRequestDto webhookRequestDto)
    {
        await _webhooksService.AddAsync(webhookRequestDto);
        return StatusCode(StatusCodes.Status201Created);
    }

    [HttpDelete]
    [Route("{id:int}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        await _webhooksService.DeleteAsync(id);
        return StatusCode(StatusCodes.Status204NoContent);
    }
}