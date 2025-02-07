namespace ECommerceWebhook.Domain.Ports;

public interface IWebhookNotifier
{
    Task NotifyAsync(string eventName, string orderNumber, IEnumerable<string> urls);
}