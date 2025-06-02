using System.ComponentModel.DataAnnotations;

namespace GiftSync.Server.Models;

public class EventParticipant : Base
{
    [Key]
    public int Id { get; set; }
    
    public int EventId { get; set; }
    public Event Event { get; set; } = null!;
    
    // A participant can be a registered User or a Guest User.
    // If UserId is null, it's a guest. GuestIdentifier would then be used.
    public int? UserId { get; set; }
    public ApplicationUser? User { get; set; }

    // For Guest Users, a unique identifier (e.g., a GUID string)
    [MaxLength(256)]
    public string? GuestIdentifier { get; set; }
    
    [MaxLength(256)]
    public string? GuestEmail { get; set; }
    
    public EventWishlist? EventWishlist { get; set; }
}