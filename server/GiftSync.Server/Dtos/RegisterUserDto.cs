using System.ComponentModel.DataAnnotations;

namespace GiftSync.Server.Dtos;

public class RegisterUserDto
{
    [Required] public string Username { get; set; }
    [Required] public string Password { get; set; }
    [Required] public string Email { get; set; }
}