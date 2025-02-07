using System.ComponentModel.DataAnnotations;

namespace ECommerceWebhook.Domain.Entities;

public class Webhook
{
    [Required]
    public int Id { get; set; }
    [Required]
    public int EventId { get; set; }
    [Required]
    [MaxLength(2048)]
    public string Url { get; set; }

    public Event Event { get; set; }
}