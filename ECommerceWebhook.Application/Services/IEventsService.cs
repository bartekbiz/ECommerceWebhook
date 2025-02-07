using ECommerceWebhook.Domain.DTOs;

namespace ECommerceWebhook.Application.Services;

public interface IEventsService
{
    Task HandleAsync(string eventName, string orderNumber);
    Task<IEnumerable<EventResponseDto>> GetAllAsync();
}