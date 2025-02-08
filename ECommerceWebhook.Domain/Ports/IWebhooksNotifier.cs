namespace ECommerceWebhook.Domain.Ports;

public interface IWebhooksNotifier
{
    Task NotifyAsync(string eventName, string orderNumber, IEnumerable<string> urls);
}