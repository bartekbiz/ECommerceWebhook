using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceWebhook.Domain.Entities;

public class Event
{
    [Key]
    public int Id { get; set; }
    [Required]
    [MaxLength(100)]
    public string Name { get; set; }

    public IEnumerable<Webhook>? Webhooks { get; set; }
}