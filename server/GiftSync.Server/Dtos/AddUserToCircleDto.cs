using System.ComponentModel.DataAnnotations;

namespace GiftSync.Server.Dtos;

public class AddUserToCircleDto
{
    [Required]
    public int UserId { get; set; }
}