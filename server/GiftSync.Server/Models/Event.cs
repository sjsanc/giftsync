using System.ComponentModel.DataAnnotations;
using GiftSync.Server.Enums;

namespace GiftSync.Server.Models;

public class Event : Base
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    [MaxLength(256)]
    public string Name { get; set; }

    [Required]
    public DateTime OccasionDate { get; set; }
    public string Description { get; set; } = string.Empty;

    [Required]
    public int OwnerId { get; set; }
    public ApplicationUser Owner { get; set; } = null!;
    
    [Required]
    public EventStatus Status { get; set; } = EventStatus.Open;
    
    public ICollection<EventParticipant> EventParticipants { get; set; } = new List<EventParticipant>();
}