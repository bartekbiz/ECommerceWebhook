using ECommerceWebhook.Application.Services;
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
    public void HandleAsync_ShouldInvokeNotifierWithProperUrls()
    {
    }
    
    [Fact]
    public void HandleAsync_ShouldFail()
    {
    }
    
    [Fact]
    public void GetAllAsync_ShouldGetAllAndMapEventsToEventResponseDtos()
    {
    }
}