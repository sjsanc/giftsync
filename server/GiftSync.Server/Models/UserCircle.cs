namespace GiftSync.Server.Models;

public class UserCircle : Base
{
    public int UserId { get; set; }
    public ApplicationUser User { get; set; } = null!;

    public int CircleId { get; set; }
    public Circle Circle { get; set; } = null!;
}