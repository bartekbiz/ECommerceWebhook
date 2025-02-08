using ECommerceWebhook.Application.Services;
using ECommerceWebhook.Domain.DTOs;
using ECommerceWebhook.Domain.Entities;
using ECommerceWebhook.Domain.Ports;
using Xunit.Abstractions;

namespace ECommerceWebhook.Tests.UnitTests.Services;

public class WebhooksServiceTests : ServiceTestsBase
{
    private readonly Mock<IWebhooksRepository> _mockWebhooksRepository;

    private readonly IWebhooksService _webhooksService;
    
    public WebhooksServiceTests(ITestOutputHelper output) : base(output)
    {
        _mockWebhooksRepository = new Mock<IWebhooksRepository>();
        
        _webhooksService = new WebhooksService(_mockWebhooksRepository.Object, Mapper);
    }
    
    [Fact]
    public async Task GetAllAsync_ShouldGetAllAndMapWebhooksToWebhookResponseDtos()
    {
        // Arrange
        const string url1 = "http://some.com/1";
        
        _mockWebhooksRepository
            .Setup(x => x.GetAllAsync())
            .ReturnsAsync([
                new Webhook()
                {
                    Id = 1,
                    EventId = 1,
                    Url = url1
                },
                new Webhook()
                {
                    Id = 2,
                    EventId = 2,
                    Url = "http://some.com/2"
                },
            ]);

        // Act
        var result = await _webhooksService.GetAllAsync();
        result = result.ToList();
        
        // Assert
        Assert.NotNull(result);
        Assert.NotEmpty(result);
        Assert.Equal(url1, result.First().Url);
        Assert.Equal(2, result.Last().Id);
    }
    
    [Fact]
    public async Task AddAsync_ShouldMapToWebhookAndInvokeAdd()
    {
        // Arrange
        const int eventId = 1;
        const string url = "http://some.com/1";
        
        var webhookDto = new WebhookRequestDto
        {
            EventId = eventId,
            Url = url
        };
        
        Webhook? addedWebhook = null;
        _mockWebhooksRepository
            .Setup(x => x.AddAsync(It.IsAny<Webhook>()))
            .Callback((Webhook w) => addedWebhook = w);

        // Act
        await _webhooksService.AddAsync(webhookDto);

        // Assert
        Assert.NotNull(addedWebhook);
        Assert.Equal(eventId, addedWebhook.EventId);
        Assert.Equal(url, addedWebhook.Url);
    }
    
    [Fact]
    public async Task DeleteAsync_ShouldProperlyInvokeDeleteFromRepository()
    {
        // Arrange
        var webhook = new Webhook
        {
            Id = 1,
            EventId = 1,
            Url = "http://some.com/1"
        };
        
        _mockWebhooksRepository
            .Setup(x => x.GetByIdAsync(webhook.Id))
            .ReturnsAsync(webhook);

        var deleteInvoked = false;
        _mockWebhooksRepository
            .Setup(x => x.DeleteAsync(webhook))
            .Callback(() => deleteInvoked = true);

        // Act
        await _webhooksService.DeleteAsync(webhook.Id);

        // Assert
        Assert.True(deleteInvoked);
    }
    
    [Fact]
    public async Task DeleteAsync_ShouldFail()
    {
        // Arrange
        const int id = 1;
        
        _mockWebhooksRepository
            .Setup(x => x.GetByIdAsync(id))
            .ReturnsAsync((Webhook?)null);

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() => _webhooksService.DeleteAsync(id));
    }
}