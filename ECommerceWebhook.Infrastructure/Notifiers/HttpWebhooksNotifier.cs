using System.Net.Http.Json;
using ECommerceWebhook.Domain.Ports;
using NLog;

namespace ECommerceWebhook.Infrastructure.Notifiers;

public class HttpWebhooksNotifier : IWebhooksNotifier
{
    private readonly HttpClient _httpClient;
    private readonly ILogger _logger;
    
    public HttpWebhooksNotifier(HttpClient httpClient, ILogger logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }
    
    public async Task NotifyAsync(string eventName, string orderNumber, IEnumerable<string> urls)
    {
        var payload = new { EventName = eventName, OrderNumber = orderNumber };

        foreach (var url in urls)
        {
            await Post(url, payload);
        }
    }

    private async Task Post(string url, object payload)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync(url, payload);
            _logger.Info($"Notification to {url} returned status code {Convert.ToInt32(response.StatusCode)} " +
                         $"{response.StatusCode}");
        }
        catch (Exception e)
        {
            _logger.Error($"Error sending notification to url {url}, details: \n{e}");
        }
    }
}