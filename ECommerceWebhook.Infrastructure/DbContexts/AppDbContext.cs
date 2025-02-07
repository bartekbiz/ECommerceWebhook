using ECommerceWebhook.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ECommerceWebhook.Infrastructure.DbContexts;

public class AppDbContext : DbContext
{
    public DbSet<Event> Events { get; set; }
    public DbSet<Webhook> Webhooks { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        CreateEvents(modelBuilder);
        CreateWebhookRegistrations(modelBuilder);
    }

    private static void CreateEvents(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Event>()
            .HasIndex(e => e.Name)
            .IsUnique();
        
        modelBuilder.Entity<Event>()
            .HasData(GenerateDummyEvents());
    }

    private static Event[] GenerateDummyEvents()
    {
        
        return 
        [
            new Event
            {
                Id = 1,
                Name = "event1"
            },
            new Event
            {
                Id = 2,
                Name = "event2"
            },
            new Event
            {
                Id = 3,
                Name = "event3"
            }
        ];
    }

    private static void CreateWebhookRegistrations(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Webhook>()
            .HasOne(w => w.Event)
            .WithMany(e => e.Webhooks)
            .HasForeignKey(w => w.EventId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Webhook>()
            .HasData(GenerateDummyWebhooks());
    }

    private static Webhook[] GenerateDummyWebhooks()
    {
        return 
        [
            new Webhook
            {
                Id = 1,
                EventId = 1,
                Url = "http://some.url/1",
            },
            new Webhook
            {
                Id = 2,
                EventId = 1,
                Url = "http://some.url/2"
            },
            new Webhook
            {
                Id = 3,
                EventId = 2,
                Url = "http://some.url/3"
            },
            new Webhook
            {
                Id = 4,
                EventId = 2,
                Url = "http://some.url/4"
            },
            new Webhook
            {
                Id = 5,
                EventId = 2,
                Url = "http://some.url/5"
            },
            new Webhook
            {
                Id = 6,
                EventId = 2,
                Url = "http://some.url/6"
            },
            new Webhook
            {
                Id = 7,
                EventId = 3,
                Url = "http://some.url/7"
            }
        ];
    }
}
