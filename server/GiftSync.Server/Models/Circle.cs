using System.ComponentModel.DataAnnotations;

namespace GiftSync.Server.Models;

public class Circle : Base
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    [MaxLength(256)]
    public string Name { get; set; }
    
    [Required]
    public int OwnerId { get; set; }
    public ApplicationUser Owner { get; set; } = null!;
    
    public ICollection<UserCircle> UserCircles { get; set; } = new List<UserCircle>();
}