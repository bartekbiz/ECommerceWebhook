using System.ComponentModel.DataAnnotations;

namespace ECommerceWebhook.Domain.DTOs;

public class WebhookRequestDto
{
    [Required]
    public int EventId { get; set; }
    [Required]
    [MaxLength(2048)]
    public string Url { get; set; }
}