using System.ComponentModel.DataAnnotations;

namespace GiftSync.Server.Models;

public class EventWishlist
{
    [Key]
    public int Id { get; set; }
    
    public int EventParticipantId { get; set; }
    public EventParticipant EventParticipant { get; set; } = null!;
    
    public ICollection<EventGift> Gifts { get; set; } = new List<EventGift>();
}