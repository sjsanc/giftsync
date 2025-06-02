using GiftSync.Server.Contexts;
using GiftSync.Server.Dtos;
using GiftSync.Server.Helpers;
using GiftSync.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace GiftSync.Server.Services;

public class EventService : IEventService
{
    private readonly ILogger<EventService> _logger;
    private readonly AppDbContext _dbContext;
    
    public EventService(ILogger<EventService> logger, AppDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    public async Task<ServiceResult<IEnumerable<EventDto>>> GetEvents()
    {
        var events = await _dbContext.Events
            .Include(e => e.Owner)
            .Include(e => e.EventParticipants)
                .ThenInclude(ep => ep.User)
            .ToListAsync();
        
        var dtos = EventDto.FromEntities(events);
        
        return ServiceResult<IEnumerable<EventDto>>.Success(dtos);
    }

    public async Task<ServiceResult<EventDto>> GetEventById(int id)
    {
        var entity = await _dbContext.Events
            .Include(e => e.Owner)
            .Include(e => e.EventParticipants)
                .ThenInclude(ep => ep.User)
            .FirstOrDefaultAsync(e => e.Id == id);

        if (entity == null)
        {
            return ServiceResult<EventDto>.Failure($"Event with id {id} was not found");
        }
        
        var dto = EventDto.FromEntity(entity);

        return ServiceResult<EventDto>.Success(dto);
    }
    
    public async Task<ServiceResult<EventDto>> CreateEvent(CreateEventDto createEventDto)
    {
        var newEvent = new Event
        {
            Name = createEventDto.Name,
            OwnerId = createEventDto.OwnerId,
            OccasionDate = createEventDto.OccasionDate,
            Description = createEventDto.Description,
        };
        
        _dbContext.Events.Add(newEvent);
        
        var ownerParticipant = new EventParticipant
        {
            Event = newEvent,
            UserId = createEventDto.OwnerId,
        };
        
        _dbContext.EventParticipants.Add(ownerParticipant);

        try
        {
            await _dbContext.SaveChangesAsync();
            var createdDto = EventDto.FromEntity(newEvent);
            return ServiceResult<EventDto>.Success(createdDto);
        }
        catch (Exception e)
        {
            return ServiceResult<EventDto>.Failure("", e);
        }
    }

    public async Task<ServiceResult> AddParticipantToEvent(int eventId, int userId)
    {
        var @event = await _dbContext.Events
            .Include(e => e.EventParticipants)
            .FirstOrDefaultAsync(e => e.Id == eventId);

        if (@event is null)
        {
            return ServiceResult.Failure($"Event with id {eventId} not found");
        }
        
        var userToAdd = await _dbContext.Users.FindAsync(userId);

        if (userToAdd is null)
        {
            return ServiceResult.Failure($"User with id {userId} not found");
        }

        if (@event.EventParticipants.Any(e => e.UserId == userId))
        {
            return ServiceResult.Failure($"User with id {userId} is already a participant of {eventId}");
        }

        var participant = new EventParticipant()
        {
            Event = @event,
            UserId = userId,
        };
        
        @event.EventParticipants.Add(participant);

        try
        {
            await _dbContext.SaveChangesAsync();
            return ServiceResult.Success();
        }
        catch (Exception e)
        {
            return ServiceResult.Failure("", e);
        }
    }

    public async Task<ServiceResult> RemoveParticipantFromEvent(int eventId, int userId)
    {
        var participant = await _dbContext.EventParticipants
            .FirstOrDefaultAsync(ep => ep.EventId == eventId && ep.UserId == userId);

        if (participant is null)
        {
            return ServiceResult.Failure($"Participant with user id {userId} not found for event {eventId}");
        }

        participant.IsDeleted = true;
        participant.DeletedAt = DateTime.UtcNow;

        try
        {
            await _dbContext.SaveChangesAsync();
            return ServiceResult.Success();
        }
        catch (Exception e)
        {
            return ServiceResult.Failure("", e);
        }
    }

    public async Task<ServiceResult> DeleteEvent(int id)
    {
        var entity = await _dbContext.Events.FindAsync(id);

        if (entity is null)
        {
            return ServiceResult.Failure($"Event with id {id} was not found");
        }
        
        entity.IsDeleted = true;
        entity.DeletedAt = DateTime.UtcNow;

        try
        {
            await _dbContext.SaveChangesAsync();
            return ServiceResult.Success();
        }
        catch (Exception e)
        {
            return ServiceResult.Failure("", e);
        }
    }
}