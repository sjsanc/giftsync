using GiftSync.Server.Models;

namespace GiftSync.Server.Dtos;

public class UserInCircleDto
{
    public int UserId { get; set; }
    public string? UserName { get; set; }
    public string? UserEmail { get; set; }
    
    public static UserInCircleDto FromUserCircle(UserCircle userCircle)
    {
        if (userCircle?.User == null) return null;
        
        return new UserInCircleDto
        {
            UserId = userCircle.UserId,
            UserName = userCircle.User.Name,
            UserEmail = userCircle.User.Email
        };
    }
}