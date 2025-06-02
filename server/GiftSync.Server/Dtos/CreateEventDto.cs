using System.ComponentModel.DataAnnotations;

namespace GiftSync.Server.Dtos;

public class CreateEventDto
{
    [Required]
    public string Name { get; set; }
    [Required]
    public int OwnerId { get; set; }
    [Required]
    public DateTime OccasionDate { get; set; }
    public string Description { get; set; } = string.Empty;
}