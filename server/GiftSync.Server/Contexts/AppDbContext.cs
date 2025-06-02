using GiftSync.Server.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GiftSync.Server.Contexts;

public class AppDbContext : IdentityDbContext<ApplicationUser, IdentityRole<int>, int>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    
    public DbSet<Circle> Circles { get; set; }
    public DbSet<UserCircle> UserCircles { get; set; }
    public DbSet<Event> Events { get; set; }
    public DbSet<EventParticipant> EventParticipants { get; set; }
    public DbSet<EventWishlist> EventWishlists { get; set; }
    public DbSet<EventGift> EventGifts { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder); // IMPORTANT: This configures the Identity schema
        
        modelBuilder.Entity<UserCircle>()
            .HasKey(uc => new { uc.UserId, uc.CircleId });
        
        modelBuilder.Entity<UserCircle>()
            .HasOne(uc => uc.User)
            .WithMany(u => u.UserCircles)
            .HasForeignKey(uc => uc.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<UserCircle>()
            .HasOne(uc => uc.Circle)
            .WithMany(c => c.UserCircles)
            .HasForeignKey(uc => uc.CircleId)
            .OnDelete(DeleteBehavior.Restrict);
        
        modelBuilder.Entity<EventParticipant>()
            .HasOne(ep => ep.Event)
            .WithMany(e => e.EventParticipants)
            .HasForeignKey(ep => ep.EventId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<EventParticipant>()
            .HasOne(ep => ep.User)
            .WithMany(u => u.EventParticipants)
            .HasForeignKey(ep => ep.UserId)
            .OnDelete(DeleteBehavior.Restrict);
        
        modelBuilder.Entity<EventWishlist>()
            .HasOne(ew => ew.EventParticipant)
            .WithOne(ep => ep.EventWishlist)
            .HasForeignKey<EventWishlist>(ew => ew.EventParticipantId);

        
        // Rename Identity tables
        modelBuilder.Entity<ApplicationUser>().ToTable("Users");
        modelBuilder.Entity<IdentityRole<int>>().ToTable("Roles");
        modelBuilder.Entity<IdentityUserRole<int>>().ToTable("UserRoles");
        modelBuilder.Entity<IdentityUserClaim<int>>().ToTable("UserClaims");
        modelBuilder.Entity<IdentityUserLogin<int>>().ToTable("UserLogins");
        modelBuilder.Entity<IdentityRoleClaim<int>>().ToTable("RoleClaims");
        modelBuilder.Entity<IdentityUserToken<int>>().ToTable("UserTokens");
    }
}