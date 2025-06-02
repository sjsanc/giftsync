using GiftSync.Server.Contexts;
using GiftSync.Server.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;

namespace GiftSync.Server.Tests;

public class EventServiceTests
{
    private readonly AppDbContext _mockDbContext;
    private readonly ILogger<EventService> _mockLogger;
    private readonly EventService _eventService;
    
    public EventServiceTests()
    {
        _mockDbContext = Substitute.For<AppDbContext>(new DbContextOptionsBuilder<AppDbContext>().Options);
        _mockLogger = Substitute.For<ILogger<EventService>>();
        _eventService = new EventService(_mockLogger, _mockDbContext);
    }

    [Fact]
    public async Task CreateEvent_CreatesEvent()
    {
    }
}