namespace GiftSync.Server.Models;

public class Base
{
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? DeletedAt { get; set; } = null;
    public bool IsDeleted { get; set; } = false;
}