using AutoMapper;
using ECommerceWebhook.Domain.DTOs;
using ECommerceWebhook.Domain.Entities;

namespace ECommerceWebhook.Application.MappingProfiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<EventRequestDto, Event>();
        CreateMap<Event, EventResponseDto>();
        CreateMap<WebhookRequestDto, Webhook>();
        CreateMap<Webhook, WebhookResponseDto>();
    }
}