using Microsoft.EntityFrameworkCore;
using PartiesApi.Models;

namespace PartiesApi.Database;

internal class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    public DbSet<Party> Parties { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<DressCode> DressCodes { get; set; }
    public DbSet<PartyRule> PartyRules { get; set; }
    public DbSet<FriendRequest> FriendRequests { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Party>()
            .HasMany(e => e.PartyMembers)
            .WithMany(e => e.MemberParties)
            .UsingEntity(join => join.ToTable("PartyMembers"));
        
        modelBuilder.Entity<Party>()
            .HasMany(e => e.PartyEditors)
            .WithMany(e => e.EditorParties)
            .UsingEntity(join => join.ToTable("PartyEditors"));
        
        modelBuilder.Entity<User>()
            .HasMany(u => u.SentRequests)
            .WithOne(r => r.FromUser)
            .HasForeignKey(r => r.FromUserId)
            .OnDelete(DeleteBehavior.Restrict);
        
        modelBuilder.Entity<User>()
            .HasMany(u => u.ReceivedRequests)
            .WithOne(r => r.ToUser)
            .HasForeignKey(r => r.ToUserId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<FriendRequest>()
            .HasKey(r => new { r.FromUserId, r.ToUserId });

        modelBuilder.Entity<Party>()
            .HasOne(e => e.Organizer);

        modelBuilder.Entity<UserFriend>()
            .HasKey(uf => new { uf.FirstUserId, uf.SecondUserId });

        modelBuilder.Entity<UserFriend>()
            .HasOne(uf => uf.FirstUser)
            .WithMany(u => u.SentFriends)
            .HasForeignKey(uf => uf.FirstUserId)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<UserFriend>()
            .HasOne(uf => uf.SecondUser)
            .WithMany(u => u.ReceivedFriends)
            .HasForeignKey(uf => uf.SecondUserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}