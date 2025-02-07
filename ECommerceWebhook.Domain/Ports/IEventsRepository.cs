using ECommerceWebhook.Domain.Entities;

namespace ECommerceWebhook.Domain.Ports;

public interface IEventsRepository
{
    Task<bool> ExistsAsync(string eventName);
    Task<IEnumerable<Event>> GetAllAsync();
}