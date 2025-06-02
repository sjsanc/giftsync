namespace GiftSync.Server.Dtos;

public class EventParticipantDto
{
    public required EventDto Event { get; set; }
    public UserDto? User { get; set; }
    public string? GuestIdentifier { get; set; }
    public string? GuestEmail { get; set; }
}