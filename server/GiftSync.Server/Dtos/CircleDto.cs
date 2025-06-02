using GiftSync.Server.Models;

namespace GiftSync.Server.Dtos;

public class CircleDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int OwnerId { get; set; }
    public UserDto? Owner { get; set; }
    public ICollection<UserInCircleDto> Members { get; set; } = new List<UserInCircleDto>();

    public static CircleDto FromEntity(Circle circle)
    {
        if (circle == null) return null;

        return new CircleDto
        {
            Id = circle.Id,
            Name = circle.Name,
            OwnerId = circle.OwnerId,
            Owner = UserDto.FromApplicationUser(circle.Owner),
            Members = circle.UserCircles?.Select(UserInCircleDto.FromUserCircle).ToList()
                      ?? []
        };
    }

    public static IEnumerable<CircleDto> FromEntities(IEnumerable<Circle> circles)
    {
        return circles?.Select(FromEntity) ?? [];
    }
}