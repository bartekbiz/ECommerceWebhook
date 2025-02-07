using ECommerceWebhook.Domain.Ports;

namespace ECommerceWebhook.Infrastructure.Notifiers;

public class WebhookNotifier : IWebhookNotifier
{
    public async Task NotifyAsync(string eventName, string orderNumber, IEnumerable<string> urls)
    {
        Console.WriteLine("Notified");
    }
}