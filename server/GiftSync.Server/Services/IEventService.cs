using GiftSync.Server.Dtos;
using GiftSync.Server.Helpers;

namespace GiftSync.Server.Services;

public interface IEventService
{
    public Task<ServiceResult<IEnumerable<EventDto>>> GetEvents();
    public Task<ServiceResult<EventDto>> GetEventById(int id);
    public Task<ServiceResult<EventDto>> CreateEvent(CreateEventDto model);
    public Task<ServiceResult> AddParticipantToEvent(int eventId, int userId);
    public Task<ServiceResult> RemoveParticipantFromEvent(int eventId, int userId);
    public Task<ServiceResult> DeleteEvent(int id);
}