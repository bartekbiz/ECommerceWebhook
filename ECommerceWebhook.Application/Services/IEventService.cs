namespace ECommerceWebhook.Application.Services;

public interface IEventService
{
    Task ProcessEventAsync(string eventName, string orderNumber);
}