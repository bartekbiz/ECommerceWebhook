using ECommerceWebhook.Domain.DTOs;

namespace ECommerceWebhook.Application.Services;

public interface IWebhooksService
{
    Task<IEnumerable<WebhookResponseDto>> GetAllAsync();
    Task AddAsync(WebhookRequestDto webhookRequestDto);
    Task DeleteAsync(int id);
}