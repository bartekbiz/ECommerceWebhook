namespace ECommerceWebhook.Domain.DTOs;

public class WebhookResponseDto
{
    public int Id { get; set; }
    public string EventId { get; set; }
    public string Url { get; set; }
}