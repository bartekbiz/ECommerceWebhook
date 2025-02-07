using System.ComponentModel.DataAnnotations;

namespace ECommerceWebhook.Domain.DTOs;

public class EventRequestDto
{
    [Required]
    [MaxLength(100)]
    public string EventName { get; set; }
    [Required] 
    [Length(20, 20)]
    public string OrderNumber { get; set; }
}