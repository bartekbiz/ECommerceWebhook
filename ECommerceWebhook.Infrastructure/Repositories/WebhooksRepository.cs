using ECommerceWebhook.Domain.Entities;
using ECommerceWebhook.Domain.Ports;
using ECommerceWebhook.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace ECommerceWebhook.Infrastructure.Repositories;

public class WebhooksRepository : IWebhooksRepository
{
    private readonly AppDbContext _dbContext;

    public WebhooksRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<IEnumerable<Webhook>> GetAllAsync()
    {
        return await _dbContext
            .Webhooks
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Webhook?> GetByIdAsync(int id)
    {
        return await _dbContext
            .Webhooks
            .AsNoTracking()
            .FirstOrDefaultAsync(w => w.Id == id);
    }

    public async Task<IEnumerable<Webhook>> GetByEventIdAsync(int eventId)
    {
        return await _dbContext
            .Webhooks
            .Where(w => w.EventId == eventId)
            .ToListAsync();

    }

    public async Task AddAsync(Webhook webhook)
    {
        await _dbContext
            .Webhooks
            .AddAsync(webhook);
        
        await _dbContext
            .SaveChangesAsync();
    }

    public async Task DeleteAsync(Webhook webhook)
    {
        _dbContext
            .Webhooks
            .Remove(webhook);
        
        await _dbContext
            .SaveChangesAsync();
    }
}