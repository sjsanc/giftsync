using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GiftSync.Server.Enums;

namespace GiftSync.Server.Models;

public class EventGift
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    public int EventWishlistId { get; set; }
    public EventWishlist EventWishlist { get; set; } = null!;
    
    [Required]
    [MaxLength(256)]
    public string Name { get; set; }

    [MaxLength(2048)]
    public string? StoreLink { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? Price { get; set; }

    public GiftPriority Priority { get; set; } = GiftPriority.Low;

    [MaxLength(500)]
    public string? Description { get; set; }
    
    [MaxLength(2048)]
    public string? ImageUrl { get; set; }
}