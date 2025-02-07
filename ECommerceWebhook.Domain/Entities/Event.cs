using System.ComponentModel.DataAnnotations;

namespace ECommerceWebhook.Domain.Entities;

public class Event
{
    [Required]
    public int Id { get; set; }
    [Required]
    [MaxLength(100)]
    public string Name { get; set; }

    public IEnumerable<Webhook> WebhookRegistrations { get; set; }
}