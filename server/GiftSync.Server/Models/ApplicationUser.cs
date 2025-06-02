using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace GiftSync.Server.Models;

public class ApplicationUser : IdentityUser<int>
{
    [MaxLength(256)]
    public string? Name { get; set; }
    
    public ICollection<UserCircle> UserCircles { get; set; } = new List<UserCircle>();
    public ICollection<EventParticipant> EventParticipants { get; set; } = new List<EventParticipant>();
}