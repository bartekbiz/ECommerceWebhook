using ECommerceWebhook.Domain.Entities;

namespace ECommerceWebhook.Domain.Ports;

public interface IWebhooksRepository
{
    Task<IEnumerable<Webhook>> GetAllAsync();
    Task<Webhook?> GetByIdAsync(int id);
    Task AddAsync(Webhook webhook);
    Task DeleteAsync(Webhook webhook);
}