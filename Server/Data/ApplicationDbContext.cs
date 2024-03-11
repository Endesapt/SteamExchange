using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Server.Models;
using System.Reflection.Metadata;

namespace Server.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Message> Messages { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Weapon> Weapons { get; set; }
        public DbSet<UserWeapon> UsersWeapons { get; set; }
        public DbSet<TradeWeapon> TradeWeapons { get; set; }
        public DbSet<Trade> Trades { get; set; }
        public DbSet<TradeComment> TradeComments { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):
        base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Message>()
                .Property(f => f.Id)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<Chat>()
                .HasAlternateKey(f => new { f.UserId1, f.UserId2 });
            modelBuilder.Entity<UserWeapon>()
                .HasKey(uw => new {uw.UserId,uw.WeaponClassId,uw.AssetId});
            modelBuilder.Entity<TradeWeapon>().HasKey(tw => new { tw.TradeId, tw.UserId, tw.WeaponClassId,tw.AssetId });
            modelBuilder.Entity<Trade>()
                .HasMany(e => e.TradeWeapons)
                .WithOne(e => e.Trade)
                .HasForeignKey(e => e.TradeId);
        }
        public override void Dispose()
        {
            base.Dispose();
        }


    }
}
