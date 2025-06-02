using System.ComponentModel.DataAnnotations;

namespace GiftSync.Server.Dtos;

public class CreateCircleDto
{
    [Required]
    public string Name { get; set; }
    
    [Required]
    public int OwnerId { get; set; }
}