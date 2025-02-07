using AutoMapper;
using ECommerceWebhook.Domain.DTOs;
using ECommerceWebhook.Domain.Ports;

namespace ECommerceWebhook.Application.Services;

public class EventsService : IEventsService
{
    private readonly IEventsRepository _eventsRepository;
    private readonly IMapper _mapper;
    private readonly IWebhooksService _webhooksService;
    private readonly IWebhookNotifier _webhookNotifier;

    public EventsService(IEventsRepository eventsRepository, IMapper mapper, 
        IWebhooksService webhooksService, IWebhookNotifier webhookNotifier)
    {
        _eventsRepository = eventsRepository;
        _mapper = mapper;
        _webhooksService = webhooksService;
        _webhookNotifier = webhookNotifier;
    }
    
    public async Task HandleAsync(string eventName, string orderNumber)
    {
        var eventEntity = await _eventsRepository.GetByNameAsync(eventName);
        if (eventEntity == null)
        {
            throw new ArgumentException($"Event \"{eventName}\" does not exist.", nameof(eventName));
        }

        var webhooksToNotify = await _webhooksService.GetByEventIdAsync(eventEntity.Id);
        var urlsToNotify = webhooksToNotify.Select(s => s.Url);
        await _webhookNotifier.NotifyAsync(eventName, orderNumber, urlsToNotify);
    }

    public async Task<IEnumerable<EventResponseDto>> GetAllAsync()
    {
        var events = await _eventsRepository.GetAllAsync();

        var result = _mapper.Map<IEnumerable<EventResponseDto>>(events);
        return result;
    }
}