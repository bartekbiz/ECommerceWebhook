using ECommerceWebhook.Domain.DTOs;
using ECommerceWebhook.Domain.Entities;

namespace ECommerceWebhook.Domain.Ports;

public interface IEventsRepository
{
    Task<IEnumerable<Event>> GetAllAsync();
    Task<Event?> GetByNameAsync(string eventName);
}