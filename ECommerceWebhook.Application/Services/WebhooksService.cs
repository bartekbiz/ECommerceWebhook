using AutoMapper;
using ECommerceWebhook.Domain.DTOs;
using ECommerceWebhook.Domain.Entities;
using ECommerceWebhook.Domain.Ports;

namespace ECommerceWebhook.Application.Services;

public class WebhooksService : IWebhooksService
{
    private readonly IWebhooksRepository _webhooksRepository;
    private readonly IMapper _mapper;

    public WebhooksService(IWebhooksRepository webhooksRepository, IMapper mapper)
    {
        _webhooksRepository = webhooksRepository;
        _mapper = mapper;
    }
    
    public async Task<IEnumerable<WebhookResponseDto>> GetAllAsync()
    {
        var webhooks = await _webhooksRepository.GetAllAsync();

        var result = _mapper.Map<IEnumerable<WebhookResponseDto>>(webhooks);
        return result;
    }

    public async Task AddAsync(WebhookRequestDto webhookRequestDto)
    {
        var webhookToAdd = _mapper.Map<Webhook>(webhookRequestDto);

        await _webhooksRepository.AddAsync(webhookToAdd);
    }

    public async Task DeleteAsync(int id)
    {
        var webhook = await _webhooksRepository.GetByIdAsync(id);

        if (webhook == null)
        {
            throw new ArgumentException($"Webhook with id {id} does not exist", nameof(id));
        }

        await _webhooksRepository.DeleteAsync(webhook);
    }
}