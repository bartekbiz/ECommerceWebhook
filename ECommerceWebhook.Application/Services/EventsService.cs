using AutoMapper;
using ECommerceWebhook.Domain.DTOs;
using ECommerceWebhook.Domain.Ports;

namespace ECommerceWebhook.Application.Services;

public class EventsService : IEventsService
{
    private readonly IEventsRepository _eventsRepository;
    private readonly IMapper _mapper;

    public EventsService(IEventsRepository eventsRepository, IMapper mapper)
    {
        _eventsRepository = eventsRepository;
        _mapper = mapper;
    }
    
    public async Task HandleAsync(string eventName, string orderNumber)
    {
        if (!await _eventsRepository.ExistsAsync(eventName))
        {
            throw new ArgumentException($"Event \"{eventName}\" does not exist.", nameof(eventName));
        }
        
        // TODO Invoke notifier
    }

    public async Task<IEnumerable<EventResponseDto>> GetAllAsync()
    {
        var events = await _eventsRepository.GetAllAsync();

        var result = _mapper.Map<IEnumerable<EventResponseDto>>(events);
        return result;
    }
}