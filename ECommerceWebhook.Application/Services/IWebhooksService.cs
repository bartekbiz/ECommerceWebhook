using ECommerceWebhook.Domain.DTOs;

namespace ECommerceWebhook.Application.Services;

public interface IWebhooksService
{
    Task<IEnumerable<WebhookResponseDto>> GetAllAsync();
    Task<IEnumerable<WebhookResponseDto>> GetByEventIdAsync(int eventId);
    Task AddAsync(WebhookRequestDto webhookRequestDto);
    Task DeleteAsync(int id);
}