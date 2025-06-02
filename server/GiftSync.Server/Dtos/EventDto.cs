using GiftSync.Server.Enums;
using GiftSync.Server.Models;

namespace GiftSync.Server.Dtos;

public class EventDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime OccasionDate { get; set; }
    public UserDto? Owner { get; set; }
    public EventStatus Status { get; set; }
    public ICollection<EventParticipantDto> Participants { get; set; }
    
    public static EventDto FromEntity(Event eventEntity)
    {
        if (eventEntity == null) return null;

        return new EventDto
        {
            Id = eventEntity.Id,
        };
    }

    public static IEnumerable<EventDto> FromEntities(IEnumerable<Event> events)
    {
        return events?.Select(FromEntity) ?? [];
    }
}