using ECommerceWebhook.Application.Services;
using ECommerceWebhook.Domain.Entities;
using ECommerceWebhook.Domain.Ports;
using Xunit.Abstractions;

namespace ECommerceWebhook.Tests.UnitTests.Services;

public class EventsServiceTests : ServiceTestsBase
{
    private readonly Mock<IEventsRepository> _mockEventsRepository;
    private readonly Mock<IWebhooksRepository> _mockWebhooksRepository;
    private readonly Mock<IWebhooksNotifier> _mockWebhooksNotifier;

    private readonly IEventsService _eventsService;
    
    public EventsServiceTests(ITestOutputHelper output) : base(output)
    {
        _mockEventsRepository = new Mock<IEventsRepository>();
        _mockWebhooksRepository = new Mock<IWebhooksRepository>();
        _mockWebhooksNotifier = new Mock<IWebhooksNotifier>();
        
        _eventsService = new EventsService(_mockEventsRepository.Object, _mockWebhooksRepository.Object, 
            _mockWebhooksNotifier.Object, Mapper);
    }
    
    [Fact]
    public async Task HandleAsync_ShouldInvokeNotifierWithProperUrls()
    {
        // Arrange
        const int eventId = 1;
        const string eventName = "event1";
        const string orderNumber = "number";
        var urls = new List<string>()
        {
            "http://some.com/1", 
            "http://some.com/1"
        };
        
        _mockEventsRepository
            .Setup(x => x.GetByNameAsync(eventName))
            .ReturnsAsync(
                new Event
                {
                    Id = eventId,
                    Name = eventName 
                });

        _mockWebhooksRepository
            .Setup(x => x.GetByEventIdAsync(eventId))
            .ReturnsAsync([
                new Webhook()
                {
                    Id = 1,
                    EventId = eventId,
                    Url = urls[0]
                },
                new Webhook()
                {
                    Id = 2,
                    EventId = eventId,
                    Url = urls[1]
                }
            ]);
        
        var notifierInvoked = false;
        _mockWebhooksNotifier
            .Setup(x => x.NotifyAsync(eventName, orderNumber, urls))
            .Callback(() => notifierInvoked = true);
        
        // Act
        await _eventsService.HandleAsync(eventName, "number");

        // Assert
        Assert.True(notifierInvoked);
    }
    
    [Fact]
    public async Task HandleAsync_ShouldFail()
    {
        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() => 
            _eventsService.HandleAsync("name", "number"));
    }
    
    [Fact]
    public async Task GetAllAsync_ShouldGetAllAndMapEventsToEventResponseDtos()
    {
        // Arrange
        const string event1Name = "event1";
        
        _mockEventsRepository
            .Setup(x => x.GetAllAsync())
            .ReturnsAsync([
                new Event
                {
                    Id = 1,
                    Name = event1Name 
                },
                new Event
                {
                    Id = 2,
                    Name = "event2" 
                }
            ]);

        // Act
        var result = await _eventsService.GetAllAsync();
        result = result.ToList();
        
        // Assert
        Assert.NotNull(result);
        Assert.NotEmpty(result);
        Assert.Equal(event1Name, result.First().Name);
        Assert.Equal(2, result.Last().Id);
    }
}