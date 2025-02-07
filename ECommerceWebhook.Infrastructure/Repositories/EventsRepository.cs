using ECommerceWebhook.Domain.DTOs;
using ECommerceWebhook.Domain.Entities;
using ECommerceWebhook.Domain.Ports;
using ECommerceWebhook.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace ECommerceWebhook.Infrastructure.Repositories;

public class EventsRepository : IEventsRepository
{
    private readonly AppDbContext _dbContext;

    public EventsRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Event>> GetAllAsync()
    {
        return await _dbContext
            .Events
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Event?> GetByNameAsync(string eventName)
    {
        return await _dbContext.Events
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.Name.Trim().ToLower() == eventName.Trim().ToLower());
    }
}