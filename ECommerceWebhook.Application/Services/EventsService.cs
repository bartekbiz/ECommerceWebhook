using AutoMapper;
using ECommerceWebhook.Domain.DTOs;
using ECommerceWebhook.Domain.Ports;

namespace ECommerceWebhook.Application.Services;

public class EventsService : IEventsService
{
    private readonly IEventsRepository _eventsRepository;
    private readonly IWebhooksRepository _webhooksRepository;
    private readonly IWebhooksNotifier _webhooksNotifier;
    private readonly IMapper _mapper;

    public EventsService(IEventsRepository eventsRepository, IWebhooksRepository webhooksRepository,
        IWebhooksNotifier webhooksNotifier, IMapper mapper)
    {
        _eventsRepository = eventsRepository;
        _webhooksRepository = webhooksRepository;
        _webhooksNotifier = webhooksNotifier;
        _mapper = mapper;
    }
    
    public async Task HandleAsync(string eventName, string orderNumber)
    {
        var eventEntity = await _eventsRepository.GetByNameAsync(eventName);
        if (eventEntity == null)
        {
            throw new ArgumentException($"Event \"{eventName}\" does not exist.", nameof(eventName));
        }

        var webhooksToNotify = await _webhooksRepository.GetByEventIdAsync(eventEntity.Id);
        var urlsToNotify = webhooksToNotify.Select(s => s.Url);
        await _webhooksNotifier.NotifyAsync(eventName, orderNumber, urlsToNotify);
    }

    public async Task<IEnumerable<EventResponseDto>> GetAllAsync()
    {
        var events = await _eventsRepository.GetAllAsync();

        var result = _mapper.Map<IEnumerable<EventResponseDto>>(events);
        return result;
    }
}