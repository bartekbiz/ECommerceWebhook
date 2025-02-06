using System.ComponentModel.DataAnnotations;

namespace ECommerceWebhook.Api.DTOs;

public class EventRequestDTO
{
    [Required]
    [MaxLength(100)]
    public string EventName { get; set; }
    [Required]
    [Length(20, 20)]
    public string OrderNumber { get; set; }
}