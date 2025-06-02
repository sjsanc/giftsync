// using System;
// using System.Collections.Generic;
// using System.ComponentModel.DataAnnotations;
// using System.ComponentModel.DataAnnotations.Schema;
//
// public enum EventStatus
// {
//     Open,
//     Closed,
//     Archived
// }
//
// public enum GiftPriority
// {
//     Low,
//     Medium,
//     High
// }
//
// public class User
// {
//     [Key]
//     public int Id { get; set; }
//
//     [Required]
//     [MaxLength(256)]
//     public string Email { get; set; }
//
//     [Required]
//     [MaxLength(256)]
//     public string PasswordHash { get; set; } // Hashed password
//
//     [MaxLength(256)]
//     public string? Name { get; set; }
//
//     public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
//
//     // Navigation properties
//     public ICollection<UserCircle> UserCircles { get; set; } = new List<UserCircle>();
//     public ICollection<EventParticipant> EventParticipants { get; set; } = new List<EventParticipant>();
//     public ICollection<Event> OwnedEvents { get; set; } = new List<Event>();
//     public GlobalWishlist? GlobalWishlist { get; set; }
//     public ICollection<GiftReaction> GiftReactions { get; set; } = new List<GiftReaction>();
//     public ICollection<GiftRecommendation> RecommendedGifts { get; set; } = new List<GiftRecommendation>(); // Gifts this user recommends
//     public ICollection<Notification> Notifications { get; set; } = new List<Notification>();
// }
//
// public class Circle
// {
//     [Key]
//     public int Id { get; set; }
//
//     [Required]
//     [MaxLength(256)]
//     public string Name { get; set; }
//
//     [Required]
//     public int OwnerId { get; set; }
//     public User Owner { get; set; } = null!;
//
//     public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
//
//     // Navigation properties
//     public ICollection<UserCircle> UserCircles { get; set; } = new List<UserCircle>();
// }
//
// public class UserCircle
// {
//     public int UserId { get; set; }
//     public User User { get; set; } = null!;
//
//     public int CircleId { get; set; }
//     public Circle Circle { get; set; } = null!;
// }
//
// public class Event
// {
//     [Key]
//     public int Id { get; set; }
//
//     [Required]
//     [MaxLength(256)]
//     public string Name { get; set; }
//
//     [Required]
//     public DateTime OccasionDate { get; set; }
//
//     [Required]
//     public int OwnerId { get; set; }
//     public User Owner { get; set; } = null!;
//
//     [Required]
//     public EventStatus Status { get; set; } = EventStatus.Open;
//
//     public bool AllowGiftRecommendations { get; set; } = true;
//     public bool IsPrivate { get; set; } = true; // Default to private (invite-only)
//
//     public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
//
//     // Navigation properties
//     public ICollection<EventParticipant> Participants { get; set; } = new List<EventParticipant>();
//     public ICollection<Notification> Notifications { get; set; } = new List<Notification>();
// }
//
// public class EventParticipant
// {
//     public int EventId { get; set; }
//     public Event Event { get; set; } = null!;
//
//     // A participant can be a registered User or a Guest User.
//     // If UserId is null, it's a guest. GuestIdentifier would then be used.
//     public int? UserId { get; set; }
//     public User? User { get; set; }
//
//     // For Guest Users, a unique identifier (e.g., a GUID string)
//     [MaxLength(256)]
//     public string? GuestIdentifier { get; set; }
//
//     // For Guest Users, their email address.
//     [MaxLength(256)]
//     public string? GuestEmail { get; set; }
//
//     // Unique link for guest access
//     [MaxLength(512)]
//     public string? GuestAccessLink { get; set; }
//
//     public DateTime JoinedAt { get; set; } = DateTime.UtcNow;
//
//     // Navigation properties
//     public EventWishlist? EventWishlist { get; set; }
//     public ICollection<GiftRecommendation> ReceivedRecommendations { get; set; } = new List<GiftRecommendation>(); // Gifts recommended TO this participant
// }
//
// public class GlobalWishlist
// {
//     [Key]
//     public int Id { get; set; }
//
//     [Required]
//     public int UserId { get; set; }
//     public User User { get; set; } = null!;
//
//     public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
//
//     // Privacy settings (e.g., Public, Private, Circles)
//     [MaxLength(50)]
//     public string? PrivacySetting { get; set; } // e.g., "Public", "Private", "Circles"
//
//     // Navigation properties
//     public ICollection<GlobalGift> Gifts { get; set; } = new List<GlobalGift>();
// }
//
// public class GlobalGift
// {
//     [Key]
//     public int Id { get; set; }
//
//     [Required]
//     public int GlobalWishlistId { get; set; }
//     public GlobalWishlist GlobalWishlist { get; set; } = null!;
//
//     [Required]
//     [MaxLength(256)]
//     public string Name { get; set; }
//
//     [MaxLength(2048)]
//     public string? StoreLink { get; set; }
//
//     [Column(TypeName = "decimal(18, 2)")]
//     public decimal? Price { get; set; }
//
//     [MaxLength(500)]
//     public string? Description { get; set; }
//
//     [MaxLength(2048)]
//     public string? ImageUrl { get; set; }
//
//     public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
//     public DateTime? LastUpdatedAt { get; set; }
//
//     // Can be imported into multiple EventWishlists
//     public ICollection<EventGift> EventGifts { get; set; } = new List<EventGift>();
// }
//
// public class EventWishlist
// {
//     [Key]
//     public int Id { get; set; }
//
//     [Required]
//     public int EventParticipantId { get; set; }
//     public EventParticipant EventParticipant { get; set; } = null!;
//
//     public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
//
//     // Navigation properties
//     public ICollection<EventGift> Gifts { get; set; } = new List<EventGift>();
// }
//
// public class EventGift
// {
//     [Key]
//     public int Id { get; set; }
//
//     [Required]
//     public int EventWishlistId { get; set; }
//     public EventWishlist EventWishlist { get; set; } = null!;
//
//     // Optional link back to GlobalGift if it was imported
//     public int? GlobalGiftId { get; set; }
//     public GlobalGift? GlobalGift { get; set; }
//
//     [Required]
//     [MaxLength(256)]
//     public string Name { get; set; }
//
//     [MaxLength(2048)]
//     public string? StoreLink { get; set; }
//
//     [Column(TypeName = "decimal(18, 2)")]
//     public decimal? Price { get; set; }
//
//     public GiftPriority? Priority { get; set; }
//
//     [MaxLength(500)]
//     public string? Description { get; set; }
//
//     [MaxLength(2048)]
//     public string? ImageUrl { get; set; }
//
//     public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
//     public DateTime? LastUpdatedAt { get; set; }
//
//     // Navigation properties
//     public ICollection<GiftReaction> Reactions { get; set; } = new List<GiftReaction>();
//     public ICollection<GiftRecommendation> Recommendations { get; set; } = new List<GiftRecommendation>(); // When this gift is a recommendation
// }
//
// public class GiftReaction
// {
//     [Key]
//     public int Id { get; set; }
//
//     [Required]
//     public int EventGiftId { get; set; }
//     public EventGift EventGift { get; set; } = null!;
//
//     [Required]
//     public int ReactorUserId { get; set; }
//     public User ReactorUser { get; set; } = null!;
//
//     // Type of reaction: "Claim", "Purchased", "Contribute"
//     [Required]
//     [MaxLength(50)]
//     public string ReactionType { get; set; }
//
//     // For "Contribute" reactions, the amount contributed
//     [Column(TypeName = "decimal(18, 2)")]
//     public decimal? ContributionAmount { get; set; }
//
//     public DateTime ReactedAt { get; set; } = DateTime.UtcNow;
// }
//
// public class GiftRecommendation
// {
//     [Key]
//     public int Id { get; set; }
//
//     [Required]
//     public int RecommenderUserId { get; set; }
//     public User RecommenderUser { get; set; } = null!;
//
//     [Required]
//     public int RecommendedToParticipantId { get; set; } // The participant whose wishlist this gift is for
//     public EventParticipant RecommendedToParticipant { get; set; } = null!;
//
//     [Required]
//     public int EventId { get; set; } // The event where this recommendation is made
//     public Event Event { get; set; } = null!;
//
//     // Details of the recommended gift
//     [Required]
//     [MaxLength(256)]
//     public string Name { get; set; }
//
//     [MaxLength(2048)]
//     public string? StoreLink { get; set; }
//
//     [Column(TypeName = "decimal(18, 2)")]
//     public decimal? Price { get; set; }
//
//     [MaxLength(500)]
//     public string? Description { get; set; }
//
//     [MaxLength(2048)]
//     public string? ImageUrl { get; set; }
//
//     // Status of the recommendation: Pending, Accepted, Dismissed
//     [MaxLength(50)]
//     public string Status { get; set; } = "Pending";
//
//     public DateTime RecommendedAt { get; set; } = DateTime.UtcNow;
//     public DateTime? StatusChangedAt { get; set; }
//
//     // Optional: If the recommendation was accepted, link to the resulting EventGift
//     public int? AcceptedEventGiftId { get; set; }
//     public EventGift? AcceptedEventGift { get; set; }
// }
//
// public class Notification
// {
//     [Key]
//     public int Id { get; set; }
//
//     [Required]
//     public int UserId { get; set; } // User receiving the notification
//     public User User { get; set; } = null!;
//
//     [Required]
//     [MaxLength(500)]
//     public string Message { get; set; }
//
//     [Required]
//     [MaxLength(100)]
//     public string Type { get; set; } // e.g., "EventInvite", "GiftClaimed", "NewRecommendation", "EventUpdate"
//
//     public int? EventId { get; set; } // Related event
//     public Event? Event { get; set; }
//
//     public int? RelatedGiftId { get; set; } // Related gift (e.g., for reactions)
//     public EventGift? RelatedGift { get; set; }
//
//     public int? RelatedRecommendationId { get; set; } // Related recommendation
//     public GiftRecommendation? RelatedRecommendation { get; set; }
//
//     public bool IsRead { get; set; } = false;
//
//     public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
// }