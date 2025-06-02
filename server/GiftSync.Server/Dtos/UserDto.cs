using GiftSync.Server.Models;

namespace GiftSync.Server.Dtos;

public class UserDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string Email { get; set; }
    
    public static UserDto FromApplicationUser(ApplicationUser user)
    {
        if (user == null) return null;
        
        return new UserDto
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email
        };
    }
}
