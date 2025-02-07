using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceWebhook.Domain.Entities;

public class Webhook
{
    [Key]
    public int Id { get; set; }
    [Required]
    public int EventId { get; set; }
    [Required]
    [MaxLength(2048)]
    public string Url { get; set; }

    public Event Event { get; set; }
}